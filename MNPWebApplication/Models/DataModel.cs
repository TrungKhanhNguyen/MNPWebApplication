using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MNPWebApplication.Models
{
    public class DataModel
    {
        private static SqlConnection _conection;
        //private static string SQL_CONNECT = @"Data Source=.;Initial Catalog=MNP2019;Integrated Security=True";
        private static string SQL_CONNECT = @"Data Source=192.168.1.25;Initial Catalog=MNP2019;User ID=newuser;Password=Nice1234";
        //private static string SQL_CONNECT = @"Data Source=192.168.70.14;Initial Catalog=MNP2019;User ID=newuser;Password=Nice1234";
        private static void OpenConection()
        {
            try
            {
                //chuỗi kết nối
                var scon1 = SQL_CONNECT;
                //  var scon1 = @"Data Source=.\SQLEXPRESS;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"; 
                if (_conection == null)
                    _conection = new SqlConnection(scon1);
                if (_conection.State != ConnectionState.Open)
                    _conection.Open();
            }
            catch (Exception)
            {
                
            }
        }
        // ngắt kết nối
        private static void CloseConection()
        {
            if (_conection == null) return;
            if (_conection.State == ConnectionState.Open)
                _conection.Close();
        }

        //hàm chứa tên thủ tục và tham số
        private static SqlCommand BuildCommand(string proceduceName, SqlParameter[] sqlParameters)
        {
            var cmd = new SqlCommand
            {
                CommandText = proceduceName,
                Connection = _conection,
                CommandType = CommandType.StoredProcedure
            };
            foreach (var sqlParameter in sqlParameters)
            {
                cmd.Parameters.Add(sqlParameter);
            }
            return cmd;
        }

        private static SqlCommand BuildIntCommand(string proceduceName, SqlParameter[] parameters)
        {
            var cmd = BuildCommand(proceduceName, parameters);
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return cmd;
        }

        // hàm thực thi thủ tục và số dòng đã thực hiện (thành công >0, ngược lại)
        // hàm này được sử dụng lúc sửa dữ liệu, xóa dữ liệu...
        public static int ExecuteNonQuery(string proceduceName, SqlParameter[] parameters)
        {
            try
            {
                OpenConection();
                var cmd = BuildCommand(proceduceName, parameters);
                cmd.CommandType = CommandType.StoredProcedure;
                var rec = cmd.ExecuteNonQuery();
                CloseConection();
                return rec;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Hàm thực thi thủ tục và trả về một danh sách (DataTable)

        public static DataTable ExecuteQuery(string procedureName)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(SQL_CONNECT))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();

                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static DataTable ExecuteQuery(string proceduceName, SqlParameter[] parameters)
        {
            try
            {
                OpenConection();
                using (var sqlDa = new SqlDataAdapter(BuildCommand(proceduceName, parameters)))
                {
                    using (var ds = new DataSet())
                    {
                        sqlDa.Fill(ds);
                        CloseConection();
                        return ds.Tables[0];
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        public DataTable GetAllStudents()
        {
            DataTable dt = new DataTable();
            string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tblStudent", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>  
        /// Get student detail by Student id  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <returns></returns>  
        public DataTable GetStudentByID(int intStudentID)
        {
            DataTable dt = new DataTable();

            string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from tblStudent where student_id=" + intStudentID, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>  
        /// Update the student details  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <param name="strStudentName"></param>  
        /// <param name="strGender"></param>  
        /// <param name="intAge"></param>  
        /// <returns></returns>  
        public int UpdateStudent(int intStudentID, string strStudentName, string strGender, int intAge)
        {
            string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update tblStudent SET student_name=@studname, student_age=@studage , student_gender=@gender where student_id=@studid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studname", strStudentName);
                cmd.Parameters.AddWithValue("@studage", intAge);
                cmd.Parameters.AddWithValue("@gender", strGender);
                cmd.Parameters.AddWithValue("@studid", intStudentID);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>  
        /// Insert Student record into DB  
        /// </summary>  
        /// <param name="strStudentName"></param>  
        /// <param name="strGender"></param>  
        /// <param name="intAge"></param>  
        /// <returns></returns>  
        public int InsertStudent(string strStudentName, string strGender, int intAge)
        {
            string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into tblStudent (student_name, student_age,student_gender) values(@studname, @studage , @gender)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studname", strStudentName);
                cmd.Parameters.AddWithValue("@studage", intAge);
                cmd.Parameters.AddWithValue("@gender", strGender);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>  
        /// Delete student based on ID  
        /// </summary>  
        /// <param name="intStudentID"></param>  
        /// <returns></returns>  
        public int DeleteStudent(int intStudentID)
        {
            string strConString = @"Data Source=WELCOME-PC\SQLSERVER2008;Initial Catalog=MyDB;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from tblStudent where student_id=@studid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studid", intStudentID);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}