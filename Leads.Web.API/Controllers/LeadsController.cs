using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Leads.Data.Contracts.UnitOfWork;

using Leads.Web.APInfrastructure.Attributes;
using Leads.Web.APInfrastructure.Contracts;
using Leads.Web.APInfrastructure.Controllers;
using Leads.Web.APInfrastructure.Helpers;
using Leads.Web.API.ViewModels;
using Leads.Web.API.ViewModels.Response;
using Newtonsoft.Json;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Leads.Entities.Submissions;


namespace Leads.Web.API.Controllers
{
    [RoutePrefix("leads")]
    [AuthorizationRequired]
    public class LeadsController : BaseApiController
    {
        public LeadsController(ILeadsUnitOfWork LeadsUnitOfWork,IApplicationSettings settings) : base(LeadsUnitOfWork,settings)
        {

        }
        /// <summary>
        /// The endpoint persists submitted data to the database.
        /// </summary>
        /// <param name="model">The submission view model.</param>
        /// <returns>
        /// Returns a flag to indicate result.
        /// </returns>
        [System.Web.Http.Route("submit")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Submit(SubmissionViewModel model)
        {
            if (model == null)
            {
                return ResponseHelper.FormatMessage(JsonConvert.SerializeObject(new ErrorResponse
                {
                    ResponseMetaData = new MetaDataResponse
                    {
                        Culture = CultureInfo.CurrentCulture,
                        ResponseCode = HttpStatusCode.NoContent,
                        ErrorMessage = "The request model is empty."
                    }
                }));
            }

            var submission = _LeadsUnitOfWork.Submissions.QueryAll().SingleOrDefault(x => x.Email == model.Email);

            if (submission == null)
            {
                var submissionModel = model.MapTo<Submission>();
                submissionModel.IpAddress = Request.GetClientIpAddress();
                submission = _LeadsUnitOfWork.Submissions.SaveOrUpdate(submissionModel);
                _LeadsUnitOfWork.SaveAnyChanges();
            }

            if (submission == null)
                return ResponseHelper.FormatMessage(JsonConvert.SerializeObject(new ErrorResponse
                {
                    ResponseMetaData = new MetaDataResponse
                    {
                        Culture = CultureInfo.CurrentCulture,
                        ResponseCode = HttpStatusCode.NoContent,
                        ErrorMessage = "Server has received the request but there is no information to send back."
                    }
                }));

            var response = new LeadsResponse
            {
                ResponseMetaData = new MetaDataResponse
                {
                    Culture = CultureInfo.CurrentCulture,
                    ResponseCode = HttpStatusCode.OK
                },
                ResponseData = true
            };
            return ResponseHelper.FormatMessage(JsonConvert.SerializeObject(response));
        }


        /// <summary>
        /// The endpoint queries whether submission entry is closed or not.
        /// </summary> 
        /// <returns>
        /// Returns a flag to indicate result.
        /// </returns>
        [System.Web.Http.Route("isClosed")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage IsClosed()
        {
            var isClosed = (DateTime.UtcNow.Date > _settings.CloseDate.Date);
            
            var response = new WithObjectDataResponse<SubmissionClosedViewModel>()
            {
                ResponseMetaData = new MetaDataResponse
                {
                    Culture = CultureInfo.CurrentCulture,
                    ResponseCode = HttpStatusCode.OK
                },
                ResponseData = new SubmissionClosedViewModel() { Closed = isClosed}
            };
            return ResponseHelper.FormatMessage(JsonConvert.SerializeObject(response));
        }
    }    
}
