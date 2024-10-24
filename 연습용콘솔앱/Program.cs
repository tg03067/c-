using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;
using 연습용콘솔앱;
using Microsoft.AspNetCore.Hosting;



namespace MssqlTestProject
{
    //class program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("asdf");
    //        Console.ReadLine();
    //    }
    //}

    //class emp
    //{
    //    private int Empno { get; set; }
    //    private string Ename { get; set; }
    //    private double Job { get; set; }
    //    private int Mgr { get; set; }
    //    private DateTime Hiredate { get; set; }
    //    private double Sal { get; set; }
    //    private double Comm { get; set; }
    //    private int Deptno { get; set; }
    //    private string JobName { get; set; }
    //    private string Email { get; set; }
    //}
    class MssqlLib
    {
        private readonly string connectText = "Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;";
        // 접속테스트
        public bool ConnectionTest()
        {
            string connectString = connectText;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // 데이터조회
        public void selectDB()
        {
            string connectString = string.Format("Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;");
            string sql = "select * from UserInfo";
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Close();
            }
        }

        // INSERT 처리
        public void InsertDB(int id, String name)
        {
            string connectString = string.Format("Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;");
            string sql = $"insert into UserInfo (id, name) values ({id},'{name}')";

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // 데이터조회
        public DataSet GetUserInfo()
        {
            string connectString = string.Format("Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;");
            string sql = "select * from [UserInfo]";
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
            }
            return ds;
        }

        // 데이터 상세 조회
        public DataSet GetOneUserById(int id)
        {
            string connectString = string.Format("Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;");
            string sql = $"select * from [UserInfo] where id = {id}";

            DataSet ds = new DataSet();

            using(SqlConnection conn = new SqlConnection(connectString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // new SqlDataAdapter(sql, conn);
                da.Fill(ds);
            }

           
            return ds;
        }
        public DataSet GetOneUserByIdAndName(int id, string name)
        {
            string connectString = string.Format("Server=192.168.0.69,1433;Database=sampledb;Trusted_Connection=True;TrustServerCertificate=True;");
            string sql = $"select top 1 * from [UserInfo] where id = {id} and name = '{name}'";

            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectString))
            { 
                conn.Open();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
            }


            return ds;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //MssqlLib db = new MssqlLib();
            //bool isConnected = db.ConnectionTest();
            //Console.WriteLine("DB 연결 성공 여부: " + isConnected);

            //db.selectDB();

            //db.InsertDB(3, "numberthree");

            //var ds = db.GetUserInfo();

            //if (ds.Tables.Count > 0)
            //{
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
            //        Console.WriteLine($"{row["id"]}, {row["name"]}");
            //    }
            //}
            CreateHostBuild(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuild(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}