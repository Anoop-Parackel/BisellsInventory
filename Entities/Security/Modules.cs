using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{
    public class Modules
    {
        public static DataSet GetModules()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                return db.ExecuteDataSet(CommandType.Text, @"select * from [dbo].[TBL_MODULES_MST]");
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Modules | GetModules()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static dynamic GetModulesObject()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, "select * from TBL_MODULES_MST where isactive=1");
                ds.Relations.Add(new DataRelation("children", ds.Tables[0].Columns["Module_id"], ds.Tables[0].Columns["parent_id"]));
                List<object> modules = new List<dynamic>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow currentRow = ds.Tables[0].Rows[i];
                    int parentSize = currentRow.GetParentRows("children").Length;
                    int childrenSize = currentRow.GetChildRows("children").Length;
                    //No parent and has children
                    if (parentSize < 1 && childrenSize > 0)
                    {

                        dynamic module = new ExpandoObject();
                        module.Name = currentRow["module_name"].ToString();
                        module.ModuleId = Convert.ToInt32(currentRow["module_id"]);
                        module.HasChildren = true;
                        module.Children = GetChildModules(currentRow, module);
                        modules.Add(module);
                    }
                    //Neither has parent nor children
                    else if(parentSize<1 && childrenSize < 1)
                    {
                        dynamic module = new ExpandoObject();
                        module.Name = currentRow["module_name"].ToString();
                        module.ModuleId = Convert.ToInt32(currentRow["module_id"]);
                        module.HasChildren = false;
                        modules.Add(module);
                    }
                }
                return modules;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Modules | GetModulesObject()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        private static dynamic GetChildModules(DataRow parent, dynamic module)
        {
            dynamic childModules = new List<dynamic>();
            if (parent.GetChildRows("children").Length > 0)
            {
                DataRow[] children = parent.GetChildRows("children");
                foreach (DataRow item in children)
                {
                    dynamic mod = new ExpandoObject();
                    mod.Name = item["module_name"].ToString();
                    mod.ModuleId = Convert.ToInt32(item["module_id"]);
                    if (item.GetChildRows("children").Length > 0)
                    {
                        mod.HasChildren = true;
                        mod.Children = GetChildModules(item, mod);
                    }
                    else
                    {
                        mod.HasChildren = false;
                    }
                    ((List<dynamic>)(childModules)).Add(mod);
                }
            }
            return childModules;
        }


    }
}
