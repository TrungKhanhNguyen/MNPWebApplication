using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MNPWebApplication.Models
{
    public class DataQuery
    {
        public DataTable GetAll()
        {
           return DataModel.ExecuteQuery("GetData");
        }
        public DataTable GetDataQuery(string msisdn, string fromdate, string todate)
        {
            SqlParameter[] p =
           {
               new SqlParameter("@msisdn",SqlDbType.NVarChar,50),
               new SqlParameter("@fromDate",SqlDbType.NVarChar,50),
               new SqlParameter("@toDate",SqlDbType.NVarChar,50),
           };
            p[0].Value = msisdn;
            p[1].Value = fromdate;
            p[2].Value = todate;

            return DataModel.ExecuteQuery("GetDataQuery", p);
        }

        public void UpdatePassword(int id,string password)
        {
            SqlParameter[] p =
          {
               new SqlParameter("@id",SqlDbType.Int),
               new SqlParameter("@password",SqlDbType.NVarChar,100),
           };
            p[0].Value = id;
            p[1].Value = password;
            DataModel.ExecuteNonQuery("UpdatePassword", p);
        }
        public DataTable Login(string username, string password)
        {
            SqlParameter[] p =
           {
               new SqlParameter("@username",SqlDbType.NVarChar,50),
               new SqlParameter("@password",SqlDbType.NVarChar,50),
           };
            p[0].Value = username;
            p[1].Value = password;
            return DataModel.ExecuteQuery("GetUser", p);
        }

        public DataTable GetMNCInfo()
        {
            return DataModel.ExecuteQuery("GetMNC");
        }
        public void GetMNCQuery(string mcc, string mnc, string brand,string ope,string status, string bands)
        {
            SqlParameter[] p =
           {
               new SqlParameter("@mcc",SqlDbType.NVarChar,50),
               new SqlParameter("@mnc",SqlDbType.NVarChar,50),
               new SqlParameter("@brand",SqlDbType.NVarChar,50),
               new SqlParameter("@operator",SqlDbType.NVarChar,100),
               new SqlParameter("@status",SqlDbType.NVarChar,50),
               new SqlParameter("@bands",SqlDbType.NVarChar,200),
           };
            p[0].Value = mcc;
            p[1].Value = mnc;
            p[2].Value = brand;
            p[3].Value = ope;
            p[4].Value = status;
            p[5].Value = bands;
            DataModel.ExecuteNonQuery("insertOrUpdateMNC", p);
        }

        public DataTable GetMNCQuery(int id,string mcc, string mnc, string brand, string ope, string status, string bands)
        {
            SqlParameter[] p =
           {
                new SqlParameter("@id",SqlDbType.Int),
               new SqlParameter("@mcc",SqlDbType.NVarChar,50),
               new SqlParameter("@mnc",SqlDbType.NVarChar,50),
               new SqlParameter("@brand",SqlDbType.NVarChar,50),
               new SqlParameter("@operator",SqlDbType.NVarChar,50),
               new SqlParameter("@status",SqlDbType.NVarChar,50),
               new SqlParameter("@brands",SqlDbType.NVarChar,50),
           };
            p[0].Value = id;
            p[1].Value = mcc;
            p[2].Value = mnc;
            p[3].Value = brand;
            p[4].Value = ope;
            p[5].Value = status;
            p[6].Value = brand;

            return DataModel.ExecuteQuery("insertOrUpdateMNC", p);
        }

        public void DeleteMNC(int id)
        {
          SqlParameter[] p =
          {
                new SqlParameter("@id",SqlDbType.Int),
           };
            p[0].Value = id;
            DataModel.ExecuteNonQuery("deleteMNC", p);
        }

        public DataTable GetLastUpdated()
        {
            return DataModel.ExecuteQuery("GetLastUpdated");
        }

        public DataTable GetListTarget()
        {
            return DataModel.ExecuteQuery("GetListTarget");
        }
        public DataTable GetTargetByPhone(string phone_number)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@phone",SqlDbType.NVarChar,50),
            };
            p[0].Value = phone_number;
            return DataModel.ExecuteQuery("GetTargetByPhone", p);
        }

        public DataTable GetTargetById(int id)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@id",SqlDbType.Int),
            };
            p[0].Value = id;
            return DataModel.ExecuteQuery("GetTargetById", p);
        }

        public void CreateTarget(string phone_number,string mobile_network)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@msisdn",SqlDbType.NVarChar,50),
                new SqlParameter("@route",SqlDbType.NVarChar,50),
            };
            p[0].Value = phone_number;
            p[1].Value = mobile_network;
            DataModel.ExecuteNonQuery("insertTarget", p);
        }

        public void CreateTarget(string phone_number,string mobile_network,int id)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@msisdn",SqlDbType.NVarChar,50),
                new SqlParameter("@route",SqlDbType.NVarChar,50),
            };
            p[0].Value = id;
            p[1].Value = phone_number;
            p[2].Value = mobile_network;
            DataModel.ExecuteNonQuery("insertTarget", p);
        }

        public void DeleteTarget(int id)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Id",SqlDbType.Int),
           };
            p[0].Value = id;
            DataModel.ExecuteNonQuery("DeleteTarget", p);
        }

    }
}