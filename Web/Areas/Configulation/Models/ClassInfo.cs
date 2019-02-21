using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Configulation.Models
{
    public class ClassInfo
    {
        public string Name { get; set; }

        public List<MemberInfo> Members { get; set; }

        public string EntityID { get; set; }

        public string DTOSourceCode { get; set; }

        public string SqlDaoSourceCode { get; set; }

        public string ControllerCode { get; set; }
        public string ViewCode { get; set; }

        public TableInfo TableInfo { get; set; }
        public string Module { get; set; }
        // constructor

        public ClassInfo(TableInfo table, string module = "")
        {
            TableInfo = table;
            Module = module;
            Name = table.Name.Replace("tbl", "").Replace("cof", "");
            Members = new List<MemberInfo>();
            EntityID = table.PrimaryKey;
            for (int i = 0; i < table.Colums.Count; i++)
            {
                MemberInfo m = new MemberInfo();
                m.Name = table.Colums[i].Name;
                m.Type = ConvertDataType(table.Colums[i].DataType);
                m.Default = ConvertDefault(table.Colums[i]);
                Members.Add(m);
            }

        }

        public void GenerateSourceCode(string fileTemplatePath)
        {
            this.DTOSourceCode = GenerateDTO(TableInfo, this, fileTemplatePath);
            this.SqlDaoSourceCode = GenerateSqlDaoSourceCode(TableInfo, this, fileTemplatePath);
        }

        public void GenerateController(string fileTemplatePath)
        {
            this.ControllerCode = GenerateControllerCode(TableInfo, this, fileTemplatePath);
            this.ViewCode = GenerateViewCode(fileTemplatePath);
        }

        public void Save(string path)
        {
            if (!Directory.Exists(path + "\\DTO"))
            {
                Directory.CreateDirectory(path + "\\DTO");
            }
            if (!Directory.Exists(path + "\\DAO"))
            {
                Directory.CreateDirectory(path + "\\DAO");
            }
            using (StreamWriter w = new StreamWriter(path + "\\DTO\\" + Name + ".cs", false, Encoding.Unicode))
            {
                w.Write(this.DTOSourceCode);
            }
            using (StreamWriter w = new StreamWriter(path + "\\DAO\\Sql" + Name + "Dao.cs", false, Encoding.Unicode))
            {
                w.Write(this.SqlDaoSourceCode);
            }
        }

        public void SaveController(string path)
        {
            if (!Directory.Exists(path + "\\Controllers"))
            {
                Directory.CreateDirectory(path + "\\Controllers");
            }
            if (!Directory.Exists(path + "\\Views"))
            {
                Directory.CreateDirectory(path + "\\Views");
            }
            if (!Directory.Exists(path + "\\Views\\" + Name))
            {
                Directory.CreateDirectory(path + "\\Views\\" + Name);
            }
            using (StreamWriter w = new StreamWriter(path + "\\Controllers\\" + Name + "Controller.cs", false, Encoding.Unicode))
            {
                w.Write(this.ControllerCode);
            }
            using (StreamWriter w = new StreamWriter(path + "\\Views\\" + Name + "\\index.cshtml", false, Encoding.Unicode))
            {
                w.Write(this.ViewCode);
            }
        }

        private string ConvertDataType(string sqlDataType)
        {
            if (sqlDataType == "float")
                return "float";
            if (sqlDataType == "int")
                return "int";
            if (sqlDataType == "bigint")
                return "Int64";
            if (sqlDataType == "char")
                return "char";
            if (sqlDataType == "nvarchar"
                || sqlDataType == "varchar"
                || sqlDataType == "nchar"
                || sqlDataType == "text"
                || sqlDataType == "ntext")
                return "string";
            if (sqlDataType == "bit")
                return "bool";
            if (sqlDataType == "tinyint")
                return "Int16";
            if (sqlDataType == "datetime" || sqlDataType == "date" || sqlDataType == "smalldatetime")
                return "DateTime";
            if (sqlDataType == "decimal" || sqlDataType == "money")
                return "decimal";
            return "";
        }

        private string ConvertDefault(ColumnInfo column)
        {
            string type = ConvertDataType(column.DataType);

            if (type == "int" || type == "Int16" || type == "Int64"
                || type == "float" || type == "decimal")
            {
                if (column.Default == "")
                    return "0";
                else
                    return column.Default.Replace("(", "").Replace(")", "");
            }

            if (type == "bool")
            {
                if (column.Default == "")
                    return "false";
                else
                {
                    if (column.Default.Replace("(", "").Replace(")", "") == "0")
                        return "false";
                    else
                        return "true";
                }
            }

            if (type == "string")
            {
                if (column.Default == "")
                    return "\"\"";
                else
                    return column.Default.Replace("(", "").Replace(")", "").Replace("'", "\"");
            }

            if (type == "char")
            {
                if (column.Default == "")
                    return "''";
                else
                    return column.Default.Replace("(", "").Replace(")", "");
            }
            if (type == "DateTime")
            {
                if (column.Default == "")
                    return "1900/01/01";
                else
                    return column.Default.Replace("(", "").Replace(")", "");
            }

            return "";
        }

        private string GenerateDTO(TableInfo tableInfo, ClassInfo classInfo, string fileTemplatePath)
        {
            using (StreamReader rd = new StreamReader(fileTemplatePath + "/dto.txt"))
            {
                string template = rd.ReadToEnd().Replace("#ClassName#", classInfo.Name);
                string mb = "";
                for (int i = 0; i < classInfo.Members.Count; i++)
                {
                    mb += "             public " + classInfo.Members[i].Type.Replace("enum", "") + " " + classInfo.Members[i].Name + " { get; set; } \n\n";
                }
                //template = Regex.Replace(template, "(#BeginFor#).*?(#EndFor#)", mb);
                return template.Replace("#Members#", mb);
            }
        }

        private string GenerateSqlDaoSourceCode(TableInfo TableInfo, ClassInfo classInfo, string fileTemplatePath)
        {
            using (StreamReader rd = new StreamReader(fileTemplatePath + "/SqlDao.txt"))
            {
                return rd.ReadToEnd().Replace("#ClassName#", classInfo.Name).Replace("#TableName#", TableInfo.Name);
            }
        }

        private string GenerateControllerCode(TableInfo TableInfo, ClassInfo classInfo, string fileTemplatePath)
        {
            using (StreamReader rd = new StreamReader(fileTemplatePath + "/controller.txt"))
            {
                return rd.ReadToEnd().Replace("#Name#", classInfo.Name).Replace("#Table#", TableInfo.Name).Replace("#Module#", classInfo.Module);
            }
        }

        private string GenerateViewCode(string fileTemplatePath)
        {
            using (StreamReader rd = new StreamReader(fileTemplatePath + "/view.txt"))
            {
                return rd.ReadToEnd();
            }
        }
    }
}
