using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UPSWCAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using UPSWCAPI.Services;

namespace UPSWCAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class EnquiryMasterController : ControllerBase
    {
       // private readonly EnquiryDbContext _context;
        //public EnquiryMasterController(EnquiryDbContext context)
        //{  
           
        //}

        private readonly IDapper _dapper;
        public EnquiryMasterController(IDapper dapper)
        {
            _dapper = dapper;
           // _context = context;
        }


        //public List<EnquiryStatus> GetEnquiryStatus()
        //{
        //    var list = _context.EnquiryStatus.ToList();
        //    return list;

        //}

        [HttpGet("GetAllStatus")]
        public List<EnquiryStatus> GetEnquiryStatus()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProcId", 2);
            var list = _dapper.GetAll<EnquiryStatus>("[SP_Enquiry]", dbparams, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        [HttpGet("GetAllTypes")]
        public List<EnquiryType> GetAllTypes()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProcId", 3);
            var list = _dapper.GetAll<EnquiryType>("[SP_Enquiry]", dbparams, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        [HttpGet("GetAllEnquiry")]
        public List<Enquiry> GetAllEnquiry()
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("ProcId", 4);
            var list = _dapper.GetAll<Enquiry>("[SP_Enquiry]", dbparams, commandType: CommandType.StoredProcedure).ToList();
            return list;
        }

        //[HttpGet("GetAllTypes")]
        //public List<EnquiryType> GetAllTypes()
        //{
        //    var list =_context.EnquiryType.ToList();
        //    return list;
        //}

        //[HttpGet("GetAllEnquiry")]
        //public List<EnquiryModel> GetAllEnquiry()
        //{
        //    var list = _context.EnquiryModel.ToList();
        //    return list;
        //}

        //[HttpPost("CreateNewEnquiry")] 
        //public EnquiryModel AddNewEnquiry(EnquiryModel Obj)
        //{
        //    Obj.createdDate = DateTime.Now;
        //    _context.EnquiryModel.Add(Obj);
        //    _context.SaveChanges();
        //    return Obj;
        //}

        //[HttpPost("UpdateEnquiry")]
        //public EnquiryModel Update(EnquiryModel Obj)
        //{
        //    var record = _context.EnquiryModel.SingleOrDefault(m => m.enquiryId == Obj.enquiryId);

        //     if(record != null)
        //    {
        //        record.resolution = Obj.resolution;
        //        record.enquiryStatusId = Obj.enquiryStatusId;
        //        _context.SaveChanges();
        //    }           

        //    return Obj;
        //}

        


        [HttpPost(nameof(Create))]
        public async Task<int> Create(Enquiry data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("enquiryId", data.enquiryId, DbType.Int32);
            dbparams.Add("enquiryTypeId", data.enquiryTypeId, DbType.Int32);
            dbparams.Add("enquiryStatusId", data.enquiryStatusId, DbType.Int32);
            dbparams.Add("customerName", data.customerName, DbType.String);
            dbparams.Add("MobileNo", data.MobileNo, DbType.String);
            dbparams.Add("email", data.email, DbType.String);          
            dbparams.Add("message", data.message, DbType.String);
            dbparams.Add("createdDate", data.createdDate, DbType.DateTime);
            dbparams.Add("resolution", data.resolution, DbType.String);
            dbparams.Add("ProcId", 1, DbType.Int32);

            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Enquiry]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

    }
}
