using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Entities.Finance
{
    public class AccountGroup
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountType { get; set; }
        public int Disable { get; set; }
        public int Depth { get; set; }
        public int ParentId { get; set; }
        public int IsAffectGP { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Company { get; set; }
        #endregion

        /// <summary>
        /// To Get the Details For Editing
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static AccountGroup GetDetails(int ID)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select Top 1 Fag_ID ID,Fag_Name Name,Fag_Description [Description],Fag_Type [Type],Fag_Disable [Status],Fag_ParentID ParentID,Fag_Depth Depth,isAffectGP from tbl_Fin_AccountGroup Where Fag_ID=" + ID).Tables[0];
                DataRow AccountGroupdata = dt.Rows[0];
                AccountGroup Account = new AccountGroup();
                Account.Id = AccountGroupdata["ID"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata["ID"]) : 0;
                Account.Name = Convert.ToString(AccountGroupdata["Name"]);
                Account.Description = Convert.ToString(AccountGroupdata["Description"]);
                Account.AccountType = Convert.ToInt32(AccountGroupdata["Type"]);
                Account.ParentId= Convert.ToInt32(AccountGroupdata["ParentID"]);
                Account.IsAffectGP= Convert.ToInt32(AccountGroupdata["isAffectGP"]);
                Account.Disable = AccountGroupdata["Status"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata["Status"]) : 0;
                return Account;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountGroup | GetDetails(int ID)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// To Get the details for the table
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDetails()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fag_ID ID,Fag_Name Name,Fag_Description [Description],Fag_Type [Type],Fag_Disable [Status],Fag_ParentID ParentID,Fag_Depth Depth,isAffectGP from tbl_Fin_AccountGroup").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountGroup |  GetDetails()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Update Function
        /// </summary>
        /// <returns></returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.AccountGroup, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "AccountGroup | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.Id == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "AccountGroup | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.Others, "AccountGroup | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"UPDATE [dbo].[tbl_Fin_AccountGroup]
                                          SET [Fag_Name] = @Fag_Name
                                             ,[Fag_Description] = @Fag_Description
                                             ,[Fag_Type] = @Fag_Type
                                             ,[Fag_Disable] = @Fag_Disable
                                             ,[Fag_ParentID] = @Fag_ParentID
                                             ,[Fag_Depth] = @Fag_Depth
                                             ,[Fag_CurSysUser] = @Fag_CurSysUser
                                             ,[Fag_CurDtTime] = @Fag_CurDtTime
                                             ,[Fag_ComID] = @Fag_ComID
                                             ,[isAffectGP] = @isAffectGP
                                        where fag_ID=@id";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Fag_Name", this.Name);
                        db.AddParameters(1, "@Fag_Disable", this.Disable);
                        db.AddParameters(2, "@Fag_Description", this.Description);
                        db.AddParameters(3, "@Fag_ParentID", this.ParentId);
                        db.AddParameters(4, "@id", this.Id);
                        db.AddParameters(5, "@Fag_CurSysUser", this.ModifiedBy);
                        db.AddParameters(6, "@Fag_Type", this.AccountType);
                        db.AddParameters(7, "@Fag_Depth", this.Depth);
                        db.AddParameters(8, "@Fag_ComID", this.Company);
                        db.AddParameters(9, "@Fag_CurDtTime", DateTime.UtcNow);
                        db.AddParameters(10, "@isAffectGP", this.IsAffectGP);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Group updated successfully", true, Type.NoError, "AccountGroup | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Group could not be updated", false, Type.Others, "AccountGroup | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Group could not be updated", false, Type.Others, "AccountGroup | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }
                }
            }
        }
        /// <summary>
        /// Delete Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.AccountGroup, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "AccountGroup | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.Id != 0)
                    {
                        db.Open();
                        string query = @"delete from tbl_fin_accountgroup where Fag_Object_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.Id);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Group deleted successfully", true, Type.NoError, "AccountGroup | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Group could not be deleted", false, Type.Others, "AccountGroup | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "AccountGroup | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "AccountGroup | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
        /// <summary>
        /// Save Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.AccountGroup, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "AccountGroup | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Group name must not be empty", false, Type.RequiredFields, "AccountGroup | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string sql = "select ISNULL(Max(fag_ID),0) fag_ID from tbl_Fin_AccountGroup";
                        int fagID = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sql));
                        string query = @"INSERT INTO [dbo].[tbl_Fin_AccountGroup]
                                       ([Fag_ID]
                                       ,[Fag_Name]
                                       ,[Fag_Description]
                                       ,[Fag_Type]
                                       ,[Fag_Disable]
                                       ,[Fag_ParentID]
                                       ,[Fag_Depth]
                                       ,[Fag_CurSysUser]
                                       ,[Fag_CurDtTime]
                                       ,[Fag_ComID]
                                       ,[isAffectGP])
                                 VALUES
                                       (@Fag_ID,
                                       @Fag_Name, 
                                       @Fag_Description, 
                                       @Fag_Type, 
                                       @Fag_Disable, 
                                       @Fag_ParentID, 
                                       @Fag_Depth, 
                                       @Fag_CurSysUser, 
                                       @Fag_CurDtTime, 
                                       @Fag_ComID, 
                                       @isAffectGP)";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Fag_ID", fagID+1);
                        db.AddParameters(1, "@Fag_Name", this.Name);
                        db.AddParameters(2, "@Fag_Description", this.Description);
                        db.AddParameters(3, "@Fag_Type", this.AccountType);
                        db.AddParameters(4, "@Fag_Disable", this.Disable);
                        db.AddParameters(5, "@Fag_ParentID", this.ParentId);
                        db.AddParameters(6, "@Fag_Depth", this.Depth);
                        db.AddParameters(7, "@Fag_CurSysUser", this.CreatedBy);
                        db.AddParameters(8, "@Fag_CurDtTime", DateTime.UtcNow);
                        db.AddParameters(9, "@Fag_ComID", this.Company);
                        db.AddParameters(10, "@isAffectGP", this.IsAffectGP);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Group saved successfully", true, Type.NoError, "AccountGroup | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Group could not be saved", false, Type.Others, "AccountGroup | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Group could not be saved", false, Type.Others, "AccountGroup | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Get Datails For Editing
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static DataTable GetAccountGroups(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fag_ID id,Fag_Name Name from tbl_Fin_AccountGroup where Fag_ComID="+CompanyId).Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountGroup | GetAccountGroups(int CompanyId)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }
        public void BindTree(TreeView treeAccountGroup,int CompanyID)
        {
            try
            {
                DBManager db = new DBManager();
                db.Open();
                DataSet ds = new DataSet();
                string SQLString = "";
                treeAccountGroup.Nodes.Clear();
                SQLString = "SELECT Fag_ParentID,Fag_Name,Fag_ID FROM tbl_Fin_AccountGroup WHERE Fag_ComID = "+CompanyID+"  ORDER BY Fag_Depth,Fag_Name";
                ds = db.ExecuteDataSet(CommandType.Text, SQLString);
                TreeNode node = new TreeNode();
                node.SelectAction = TreeNodeSelectAction.None;
                TreeNode childNode = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode1 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode2 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode3 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode4 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode5 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode6 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode7 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode8 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode subChildNode9 = new TreeNode() { SelectAction = TreeNodeSelectAction.None };
                TreeNode mainNode = new TreeNode("Account Group", "") { SelectAction = TreeNodeSelectAction.None };
                mainNode.ToolTip = "Account Group";
                DataRow[] rows = ds.Tables[0].Select("Fag_ParentID = 0");
                if (rows.Length > 0)
                {
                    foreach (DataRow singleRow in rows)
                    {
                        node = new TreeNode(Convert.ToString(singleRow["Fag_Name"]), Convert.ToString(singleRow["Fag_ID"]));
                        node.ToolTip = Convert.ToString(singleRow["Fag_Name"]);
                        mainNode.ChildNodes.Add(node);
                        DataRow[] rowsSub = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRow["Fag_ID"]));
                        if (rowsSub.Length > 0)
                        {
                            foreach (DataRow singleRowSub in rowsSub)
                            {
                                childNode = new TreeNode(Convert.ToString(singleRowSub["Fag_Name"]), Convert.ToString(singleRowSub["Fag_ID"]));
                                childNode.ToolTip = Convert.ToString(singleRowSub["Fag_Name"]);
                                node.ChildNodes.Add(childNode);
                                DataRow[] rowsSubChild = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSub["Fag_ID"]));
                                if (rowsSubChild.Length > 0)
                                {
                                    foreach (DataRow singleRowSubChild in rowsSubChild)
                                    {
                                        subChildNode = new TreeNode(Convert.ToString(singleRowSubChild["Fag_Name"]), Convert.ToString(singleRowSubChild["Fag_ID"]));
                                        subChildNode.ToolTip = Convert.ToString(singleRowSubChild["Fag_Name"]);
                                        childNode.ChildNodes.Add(subChildNode);
                                        DataRow[] rowsSubChild1 = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSubChild["Fag_ID"]));
                                        if (rowsSubChild1.Length > 0)
                                        {
                                            foreach (DataRow singleRowSubChild1 in rowsSubChild1)
                                            {
                                                subChildNode1 = new TreeNode(Convert.ToString(singleRowSubChild1["Fag_Name"]), Convert.ToString(singleRowSubChild1["Fag_ID"]));
                                                subChildNode1.ToolTip = Convert.ToString(singleRowSubChild1["Fag_Name"]);
                                                subChildNode.ChildNodes.Add(subChildNode1);
                                                DataRow[] rowsSubChild2 = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSubChild1["Fag_ID"]));
                                                if (rowsSubChild2.Length > 0)
                                                {
                                                    foreach (DataRow singleRowSubChild2 in rowsSubChild2)
                                                    {
                                                        subChildNode2 = new TreeNode(Convert.ToString(singleRowSubChild2["Fag_Name"]), Convert.ToString(singleRowSubChild2["Fag_ID"]));
                                                        subChildNode2.ToolTip = Convert.ToString(singleRowSubChild2["Fag_Name"]);
                                                        subChildNode1.ChildNodes.Add(subChildNode2);
                                                        DataRow[] rowsSubChild3 = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSubChild2["Fag_ID"]));
                                                        if (rowsSubChild3.Length > 0)
                                                        {
                                                            foreach (DataRow singleRowSubChild3 in rowsSubChild3)
                                                            {
                                                                subChildNode3 = new TreeNode(Convert.ToString(singleRowSubChild3["Fag_Name"]), Convert.ToString(singleRowSubChild3["Fag_ID"]));
                                                                subChildNode3.ToolTip = Convert.ToString(singleRowSubChild3["Fag_Name"]);
                                                                subChildNode2.ChildNodes.Add(subChildNode3);
                                                                DataRow[] rowsSubChild4 = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSubChild3["Fag_ID"]));
                                                                if (rowsSubChild4.Length > 0)
                                                                {
                                                                    foreach (DataRow singleRowSubChild4 in rowsSubChild4)
                                                                    {
                                                                        subChildNode4 = new TreeNode(Convert.ToString(singleRowSubChild4["Fag_Name"]), Convert.ToString(singleRowSubChild4["Fag_ID"]));
                                                                        subChildNode4.ToolTip = Convert.ToString(singleRowSubChild4["Fag_Name"]);
                                                                        subChildNode3.ChildNodes.Add(subChildNode4);
                                                                        DataRow[] rowsSubChild5 = ds.Tables[0].Select("Fag_ParentID = " + Convert.ToString(singleRowSubChild4["Fag_ID"]));
                                                                        if (rowsSubChild5.Length > 0)
                                                                        {
                                                                            foreach (DataRow singleRowSubChild5 in rowsSubChild5)
                                                                            {
                                                                                subChildNode5 = new TreeNode(Convert.ToString(singleRowSubChild5["Fag_Name"]), Convert.ToString(singleRowSubChild5["Fag_ID"]));
                                                                                subChildNode5.ToolTip = Convert.ToString(singleRowSubChild5["Fag_Name"]);
                                                                                subChildNode4.ChildNodes.Add(subChildNode5);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                treeAccountGroup.Nodes.Add(mainNode);
                treeAccountGroup.CollapseAll();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountGroup | BindTree(TreeView treeAccountGroup,int CompanyID)");
            }
        }
        public static dynamic GetGroupData(int GroupId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataTable AccountGroupdata = db.ExecuteDataSet(CommandType.Text, @"select top 1 Fag_ID,Fag_Name,Fag_Type,Fag_ParentID,Fag_Depth,Fag_ComID,isnull(Fag_Description,'') Fag_Description,Fag_Disable,isAffectGP from tbl_Fin_AccountGroup where Fag_ID = " + GroupId).Tables[0];
                dynamic Account = new ExpandoObject();
                Account.ID = AccountGroupdata.Rows[0]["Fag_ID"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata.Rows[0]["Fag_ID"]) : 0;
                Account.Name = Convert.ToString(AccountGroupdata.Rows[0]["Fag_Name"]);
                Account.Description = Convert.ToString(AccountGroupdata.Rows[0]["Fag_Description"]);
                Account.AccountType = Convert.ToInt32(AccountGroupdata.Rows[0]["Fag_Type"]);
                Account.ParentId = Convert.ToInt32(AccountGroupdata.Rows[0]["Fag_ParentID"]);
                Account.IsDisable = Convert.ToInt32(AccountGroupdata.Rows[0]["Fag_Disable"]);
                Account.isAffectGP = AccountGroupdata.Rows[0]["isAffectGP"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata.Rows[0]["isAffectGP"]) : 0;
                return Account;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountGroup | GetGroupData(int GroupId)");
                return null;
            }
        }

        public static dynamic GetTree(int CompanyID)
        {
            try
            {
                DBManager db = new DBManager();
                db.Open();
                DataSet ds = new DataSet();
                string SQLString = "";
                //treeAccountGroup.Nodes.Clear();
                SQLString = @"select 0 fag_id,'Account Group' Fag_Name,NULL Fag_ParentID
                              union all
                              select fag_id,Fag_Name,Fag_ParentID from tbl_Fin_AccountGroup where Fag_ComID="+CompanyID;
                ds = db.ExecuteDataSet(CommandType.Text, SQLString);
                ds.Relations.Add(new DataRelation("children", ds.Tables[0].Columns["Fag_ID"], ds.Tables[0].Columns["Fag_ParentID"]));
                List<object> group = new List<object>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow currentRow = ds.Tables[0].Rows[i];
                    int parentSize = currentRow.GetParentRows("children").Length;
                    int childrenSize = currentRow.GetChildRows("children").Length;
                    //No parent and has children
                    if (parentSize < 1 && childrenSize > 0)
                    {

                        dynamic module = new ExpandoObject();
                        module.Name = currentRow["Fag_Name"].ToString();
                        module.ModuleId = Convert.ToInt32(currentRow["Fag_ID"]);
                        module.HasChildren = true;
                        module.Children = GetChildModules(currentRow, module);
                        group.Add(module);
                    }
                    //Neither has parent nor children
                    else if (parentSize < 1 && childrenSize < 1)
                    {
                        dynamic module = new ExpandoObject();
                        module.Name = currentRow["Fag_Name"].ToString();
                        module.ModuleId = Convert.ToInt32(currentRow["Fag_ID"]);
                        module.HasChildren = false;
                        group.Add(module);
                    }
                }
                return group;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountGroup |  GetTree(int CompanyID)");
                return null;
            }
        }

        private static dynamic GetChildModules(DataRow parent, dynamic group)
        {
            dynamic childModules = new List<dynamic>();
            if (parent.GetChildRows("children").Length > 0)
            {
                DataRow[] children = parent.GetChildRows("children");
                foreach (DataRow item in children)
                {
                    dynamic mod = new ExpandoObject();
                    mod.Name = item["Fag_Name"].ToString();
                    mod.ModuleId = Convert.ToInt32(item["Fag_ID"]);
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
