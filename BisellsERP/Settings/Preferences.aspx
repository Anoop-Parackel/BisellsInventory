<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="BisellsERP.Settings.Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #wrapper, body {
            overflow: hidden;
        }

        .settings-wrap > div {
            height: calc(100vh - 75px);
        }

            .settings-wrap > div .nav-custom {
                height: calc(100vh - 135px);
                background-color: #fff;
                box-shadow: 0 2px 1px 0 #ccc;
            }

        .settings-wrap .nav-custom > li {
            border-bottom: 1px solid #f3f3f3;
        }

            .settings-wrap .nav-custom > li > a {
                color: #90A4AE !important;
            }

                .settings-wrap .nav-custom > li > a:hover {
                    color: #757575 !important;
                }

            .settings-wrap .nav-custom > li.active > a {
                color: #3e95cd !important;
            }

            .settings-wrap .nav-custom > li.active {
                border-left: 3px solid #3e95cd;
            }

            .settings-wrap .nav-custom > li > a:focus, .nav-custom > li > a:hover {
                background-color: transparent;
            }

            .settings-wrap .nav-custom > li > a {
                line-height: 65px;
            }

        .tab-content > .tab-pane > .panel {
            height: calc(100vh - 133px);
            box-shadow: 0 2px 1px 0 #ccc;
        }

        .organisation-wrap {
            height: calc(100vh - 160px);
            padding-bottom: 20px;
        }

        .terms-conditions {
            height: calc(100vh - 204px);
            padding-bottom: 20px;
        }

        #allowPriceEditingSetting {
            height: calc(100vh - 202px);
        }

        .sett-title {
            border-bottom: 1px dashed #ececec;
            padding-top: 20px;
            padding-bottom: 5px;
            margin-top: 0;
            margin-bottom: 15px;
            color: #3e95cd;
            margin-left: -5px;
        }

        #v-1 label, #v-2 label, #v-3 label, #v-4 label, #v-5 label {
            color: #78909c;
            font-weight: 100;
            text-align: left;
            font-size: 12px;
        }

            #v-1 label > i, #v-2 label > i, #v-3 label > i, #v-3 label > i {
                margin-left: 2px;
                color: #B0BEC5;
            }

        #v-1 .form-group, #v-2 .form-group, #v-3 .form-group, #v-4 .form-group {
            margin-bottom: 5px;
        }

        /* add a little bottom space under the images */
        .thumbnail {
            margin-bottom: 7px;
            border: 3px solid #eee;
        }

            .thumbnail.selected {
                border: 3px solid #4c9cd0;
            }

            .thumbnail > img {
                min-height: 150px;
            }

        /* Inner Horizontal Tab */
        .nav-inner.nav-tabs > li > a, .nav.tabs-vertical > li > a {
            line-height: 40px;
        }
        /*editable div*/
        .editable-div {
            max-height: 500px;
            border: solid 1px #ececec;
            height: 175px;
            overflow: auto;
        }

        .note-editable {
            min-height: 254px;
        }

        .mt-chk {
            margin-top: 199px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="settings-wrap">
        <asp:HiddenField runat="server" ClientIDMode="Static" Value="0" ID="hdSection" />
        <div class="col-sm-2 p-0">
            <h4 class="p-l-10">Settings & Preferences</h4>
            <div class="tabs-vertical-env">
                <ul class="nav nav-custom">
                    <li class="active">
                        <a id="tabOrgProfile" href="#v-1" data-toggle="tab" aria-expanded="false">Organisation Profile</a>
                    </li>
                    <li class="">
                        <a id="tabGenSetting" href="#v-2" data-toggle="tab" aria-expanded="false">General Settings</a>
                    </li>
                    <li class="">
                        <a id="tabFinSetting" href="#v-3" data-toggle="tab" aria-expanded="false">Financial Settings</a>
                    </li>
                    <li class="">
                        <a id="tabEmailSetting" href="#v-4" data-toggle="tab" aria-expanded="true">Email Settings</a>
                    </li>
                    <li class="">
                        <a id="tabInvoiceSetting" href="#v-5" data-toggle="tab" aria-expanded="true">Invoice Settings</a>
                    </li>
                </ul>
            </div>
        </div>

        <div class="col-sm-10 p-0">
            <div class="col-md-12 m-t-40">
                <div class="tab-content">

                    <%-- ORGANISATION SETTINGS --%>
                    <div class="tab-pane active" id="v-1">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="organisation-wrap">

                                    <div class="col-sm-6">
                                        <h5 class="sett-title p-t-0">Company Info</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name of Company<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Name Of the Company"></i></label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtCompanyName" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Email ID</label>
                                                        <div class="col-sm-8">
                                                            <input type="email" id="txtCompanyEmail" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Type of Firm</label>
                                                        <div class="col-sm-8">
                                                            <select class="form-control input-sm" id="ddlCompanyType">
                                                                <option value="Private">Private</option>
                                                                <option value="Manufacturers">Manufacturers</option>
                                                                <option value="Hospitals">Hospitals</option>
                                                                <option value="Retail">Retail</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <%-- Company Registration Details --%>
                                        <h5 class="sett-title ">Registration Details</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Registration ID 1</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtRegId1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Registration ID 2</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtRegId2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Registration ID 3</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtRegId3" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- Company Contact Details --%>
                                        <h5 class="sett-title">Contact Details</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Office Number</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtOfficeNo" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone 1</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtPhone1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Phone 2</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtphone2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Pin Code</label>
                                                        <div class="col-sm-8">
                                                            <input type="number" id="txtPincode" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- Contact Address Details --%>
                                        <h5 class="sett-title">Address</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Address line 1</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtAddress1" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">line 2</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtAddress2" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Country</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlCountryList" class="form-control input-sm">
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">State</label>
                                                        <div class="col-sm-8">
                                                            <select id="ddlState" class="form-control input-sm">
                                                                <option value="Kerala">Kerala</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">City</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtCity" class="form-control input-sm" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-group m-t-20">
                                                    <button type="button" id="btnSaveOrgProfile" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Organisation Profile</span></button>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-lg-1 hidden-sm hidden-xs"></div>

                                    <div class="col-sm-6 col-lg-4 text-center">
                                        <asp:Image ID="imgphoto" ClientIDMode="Static" runat="server" ImageUrl="../Theme/images/logobisellsjpg.jpg" CssClass="w-100 m-t-40"></asp:Image>
                                        <div class="fileUpload btn btn-default btn-sm waves-effect waves-light m-t-10">
                                            <span><i class="ion-upload m-r-5"></i>Upload Logo</span>
                                            <asp:FileUpload ID="FileUpload" ClientIDMode="Static" runat="server" CssClass="upload" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <%-- GENERAL SETTINGS --%>
                    <div class="tab-pane" id="v-2">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="sett-title p-t-0">General Settings</h5>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Daylight Savings<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Here You can change the Daylight Saving Time"></i></label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtDaylightSaving" class="form-control input-sm" />
                                                </div>
                                                <small class="col-sm-8 col-sm-offset-4 text-muted m-t-5"><em>Here you can change the daylight Time settings. </em></small>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Auto Round Off</label>
                                                <div class="col-sm-8">
                                                    <div class="checkbox checkbox-success">
                                                        <input id="chkAutoRoundOff" type="checkbox" />
                                                        <label for="checkbox3">
                                                        </label>

                                                    </div>

                                                </div>
                                                <small class="col-sm-8 col-sm-offset-4 text-muted m-t-5"><em>Auto Round Off helps for Automated Round Off calculation. </em></small>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Currency Symbol <i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Here You can change the Currency Symbol"></i></label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtCurrencySymbol" class="form-control input-sm" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="btn-group m-t-20">
                                            <button type="button" id="btnUpdateGenSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update General Settings</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- FINANCIAL SETTINGS --%>
                    <div class="tab-pane" id="v-3">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="sett-title p-t-0">Financial Settings</h5>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Fiscal Year<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="You can change the Finantial Year from Here."></i></label>
                                                <div class="col-sm-8">
                                                    <select id="ddlFinantialYear" class="form-control input-sm">
                                                        <option value="0">JAN-DEC</option>
                                                        <option value="1">APRIL-MARCH</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Expense group ID</label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtExpenceGroupId" class="form-control input-sm" />
                                                </div>
                                                <small class="col-sm-8 col-sm-offset-4 text-muted m-t-5">Expense Group Id's are Normally separated using "|". Eg: 45|44 </small>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="btn-group m-t-20">
                                            <button type="button" id="btnUpdateFinSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Financial Settings</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- EMAIL SETTINGS --%>
                    <div class="tab-pane" id="v-4">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="sett-title p-t-0">Email Settings</h5>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Email ID </label>
                                                <div class="col-sm-8">
                                                    <input id="txtSettingEmail" type="email" class="form-control input-sm" />
                                                </div>

                                                <small class="col-sm-8 col-sm-offset-4 text-muted m-t-5"><em>This Email-Id will be used for sending mail for all Purpose</em></small>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Password</label>
                                                <div class="col-sm-8">
                                                    <input type="password" id="txtEmailPassword" class="form-control input-sm" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Email Host&nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="SMTP Host for Email Sending."></i></label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtHost" class="form-control input-sm" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-sm-4">Port</label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtPort" class="form-control input-sm" />
                                                </div>
                                                <small class="col-sm-8 col-sm-offset-4 text-muted m-t-5"><em>The Port Id for sending the Email</em></small>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="btn-toolbar m-t-20">
                                            <button type="button" id="btnUpdateEmailSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Email Preference</span></button>
                                            <button type="button" id="btnverifyEmailSetting" class="btn btn-default waves-effect waves-light btn-xs"><i class="fa fa-binoculars"></i>&nbsp;<span>Test Connection</span></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- INVOICE SETTINGS --%>
                    <div class="tab-pane" id="v-5">
                        <div class="panel">
                            <div class="panel-body p-0" style="overflow-x: hidden;">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <ul class="nav nav-inner nav-tabs navtab-bg">
                                            <li class="active">
                                                <a href="#inv-template" data-toggle="tab" aria-expanded="false">
                                                    <span>Invoice Template</span>
                                                </a>
                                            </li>
                                            <li class="">
                                                <a href="#tAndCSetting" id="tandCTab" data-toggle="tab" aria-expanded="true">
                                                    <span>Terms & Condition Settings</span>
                                                </a>
                                            </li>
                                            <li class="">
                                                <a href="#allowPriceEditingSetting" id="priceEditingTab" data-toggle="tab" aria-expanded="true">
                                                    <span>Others</span>
                                                </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content m-b-0 p-b-0">
                                            <div class="tab-pane active" id="inv-template">
                                                <div class="row">

                                                    <%--<h5 class="sett-title p-t-0">Invoice Templates</h5>--%>
                                                    <div class="row">
                                                        <div class="form-horizontal">
                                                            <div class="form-group hidden">
                                                                <label class="control-label col-sm-2">Template Type </label>
                                                                <div class="col-sm-5">
                                                                    <input type="radio" id="GST" name="templateType" value="" />
                                                                    <label class="control-label col-sm-4">GST </label>

                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="radio" id="VAT" name="templateType" value="" />
                                                                    <label class="control-label col-sm-4">VAT </label>
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="radio" id="GST2" name="templateType" value="" />
                                                                    <label class="control-label col-sm-4">GST2 </label>
                                                                </div>
                                                                  <div class="col-sm-5">
                                                                    <input type="radio" id="VAT2" name="templateType" value="" />
                                                                    <label class="control-label col-sm-4">VAT2 </label>
                                                                </div>
                                                                   <div class="col-sm-5">
                                                                    <input type="radio" id="VAT3" name="templateType" value="" />
                                                                    <label class="control-label col-sm-4">VAT3 </label>
                                                                </div>
                                                            </div>
                                                            <%-- test --%>
                                                            <div class="container">
                                                                <div class="row">
                                                                    <div class="col-xs-4">
                                                                        <a href="#" id="vatTemp" class="thumbnail">
                                                                            <img src="..\Theme\images\vat.PNG" class="img-responsive" />
                                                                        </a>
                                                                        <label class="hidden" id="lblvatTempTemplateNumber">A</label>
                                                                        <p class="text-center">
                                                                            <button type="button" id="prevVat" class="btn btn-default"><i class="fa fa-eye" data-toggle="tooltip" data-trigger="hover" title="Preview this Image."></i></button>
                                                                            &nbsp;&nbsp;<em><b>VAT</b> Enabled Invoice Template</em>
                                                                        </p>

                                                                    </div>
                                                                    <div class="col-xs-4">
                                                                        <a href="#" id="gstTemp" class="thumbnail">
                                                                            <img src="..\Theme\images\gst.PNG" class="img-responsive" />
                                                                        </a>
                                                                        <label class="hidden" id="lblgstTempTemplateNumber">A</label>

                                                                        <p class="text-center">
                                                                            <button type="button" id="prevGst" class="btn btn-default"><i class="fa fa-eye" data-toggle="tooltip" data-trigger="hover" title="Preview this Image."></i></button>
                                                                            &nbsp;&nbsp;<em><b>GST</b> Enabled Invoice Template</em>
                                                                        </p>

                                                                    </div>
                                                                    <div class="col-xs-4">
                                                                        <a href="#" id="gstTempTwo" class="thumbnail">
                                                                            <img src="..\Theme\images\gst.PNG" class="img-responsive" />
                                                                        </a>
                                                                        <label class="hidden" id="lblgstTempTemplateTwoNumber">C</label>

                                                                        <p class="text-center">
                                                                            <%--//gst2 is vat c--%>
                                                                            <button type="button" id="prevGstTwo" class="btn btn-default"><i class="fa fa-eye" data-toggle="tooltip" data-trigger="hover" title="Preview this Image."></i></button>
                                                                            &nbsp;&nbsp;<em><b>VAT</b> Enabled Invoice Template 2</em>
                                                                        </p>

                                                                    </div>
                                                                    
                                                                         <div class="col-xs-4 p-t-5">
                                                                        <a href="#" id="vatTemp2" class="thumbnail">
                                                                            <img src="..\Theme\images\gst.PNG" class="img-responsive" />
                                                                        </a>
                                                                        <label class="hidden" id="lblvatTempTemplateTwoNumber">D</label>

                                                                        <p class="text-center">
                                                                            <%--//gst2 is vat c--%>
                                                                            <button type="button" id="prevVATTwo" class="btn btn-default"><i class="fa fa-eye" data-toggle="tooltip" data-trigger="hover" title="Preview this Image."></i></button>
                                                                            &nbsp;&nbsp;<em><b>VAT</b> Enabled Invoice Template 3</em>
                                                                        </p>

                                                                    </div>
                                                                  <div class="col-xs-4 p-t-5">
                                                                        <a href="#" id="vatTemp3" class="thumbnail">
                                                                            <img src="..\Theme\images\gst.PNG" class="img-responsive" />
                                                                        </a>
                                                                        <label class="hidden" id="lblvatTempTemplateThreeNumber">E</label>

                                                                        <p class="text-center">
                                                                          
                                                                            <button type="button" id="prevVATThree" class="btn btn-default"><i class="fa fa-eye" data-toggle="tooltip" data-trigger="hover" title="Preview this Image."></i></button>
                                                                            &nbsp;&nbsp;<em><b>VAT</b> Enabled Invoice Template 4</em>
                                                                        </p>

                                                                    </div>
                                                                    <%--   <div class="gallery_product col-lg-4 col-md-4 col-sm-4 col-xs-6 filter hdpe">
                                                     <img src="..\Theme\images\gst.PNG" class="img-responsive"/>
                                                 </div>--%>
                                                                </div>
                                                            </div>
                                                            <%-- test end --%>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="btn-group m-t-20">
                                                            <button type="button" id="btnUpdateInvTempSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Invoice Settings</span></button>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <%--  Terms and Condition Settings--%>
                                            <div class="tab-pane " id="tAndCSetting">

                                                <div class="row terms-conditions">
                                                    <%-- for purchase order --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title p-t-0">Purchase Order &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="poDiv"></div>
                                                        <div class="checkbox checkbox-success m-t-10 m-l-10">
                                                            <input id="chkPoTerms" type="checkbox" />
                                                            <label for="chkPoTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Purchase Order."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for purchase Request --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Purchase Request &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="prqDiv"></div>
                                                        <div class="checkbox checkbox-success m-t-10 m-l-10">
                                                            <input id="chkPrqTerms" type="checkbox" />
                                                            <label for="chkPrqTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Purchase Request."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for purchase entry --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Purchase Entry &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="peDiv"></div>
                                                        <div class="checkbox checkbox-success m-t-10 m-l-10">
                                                            <input id="chkPeTerms" type="checkbox" />
                                                            <label for="chkPeTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Purchase Entry."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for purchase Return --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Purchase Return &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="prDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkPrTerms" type="checkbox" />
                                                            <label for="chkPrTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Purchase Return."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for purchase Indent --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Purchase Indent &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="piDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkPiTerms" type="checkbox" />
                                                            <label for="chkPiTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Purchase Indent."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for GRN --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Goods Reciept Note &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="grnDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkgrnTerms" type="checkbox" />
                                                            <label for="chkgrnTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in GRN."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for TransferOut --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Transfer Out &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="toDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chktoTerms" type="checkbox" />
                                                            <label for="chktoTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in TransferOut."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for TransferIn --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Transfer In &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="tiDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chktiTerms" type="checkbox" />
                                                            <label for="chktiTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in TransferIn."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for Damage --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Damage &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="damDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkdamTerms" type="checkbox" />
                                                            <label for="chkdamTerms" class="control-label">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Damage."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for Sales Request --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Sales Request &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="SrqDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkSrqTerms" type="checkbox" />
                                                            <label for="chkSrqTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Request."></i></label>
                                                        </div>
                                                    </div>


                                                    <%-- for Sales Order --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Sales Order &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="SoDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkSoTerms" type="checkbox" />
                                                            <label for="chkSoTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Order."></i></label>
                                                        </div>
                                                    </div>

                                                    <%-- for Sales Entry --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Sales Entry &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="SeDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkSeTerms" type="checkbox" />
                                                            <label for="chkSeTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Entry."></i></label>
                                                        </div>
                                                    </div>


                                                    <%-- for Sales Return --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title ">Sales Return &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="SrDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkSrTerms" type="checkbox" />
                                                            <label for="chkSrTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Entry."></i></label>
                                                        </div>
                                                    </div>

                                                    
                                                    <%-- for Tax Invoice --%>
                                                    <div class="col-sm-12 hidden">
                                                        <h5 class="sett-title ">Tax Invoice &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="TiDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkTiTerms" type="checkbox" />
                                                            <label for="chkTiTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Quotaion."></i></label>
                                                        </div>
                                                    </div>

                                                     <%-- for Sales Quotation --%>
                                                    <div class="col-sm-12 hidden">
                                                        <h5 class="sett-title ">Sales Quotation &nbsp;<span class="btnEditTcPo"><i class="fa fa-edit "></i></span></h5>
                                                        <div style="max-height: 500px" data-text="Preview here.." class="editable-div" id="SalesDiv"></div>
                                                        <div class="checkbox checkbox-success  m-t-10 m-l-10">
                                                            <input id="chkSalesTerms" type="checkbox" />
                                                            <label for="chkSalesTerms" class="control-label col-sm-5">Enable Terms and conditions &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling Updated Terms and conditions in Sales Quotaion."></i></label>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-12">
                                                        <div class="btn-group m-t-20">
                                                            <button type="button" id="btnUpdateTermsAndCondetionSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Terms & Conditions Settings</span></button>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <%-- OTHERS TAB SETTINGS --%>
                                            <div class="tab-pane " id="allowPriceEditingSetting">
                                                <div class="col-sm-12">
                                                    <%-- Description Settings --%>
                                                    <div class="col-sm-12">
                                                        <h5 class="sett-title p-t-0">Sales Invoice Settings</h5>
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label class="control-label col-sm-4 ">Enable Description</label>
                                                                <div class="col-sm-8">
                                                                    <div class="checkbox checkbox-success">
                                                                        <input id="chkDescription" type="checkbox" />
                                                                        <label for="chkDescription">
                                                                        </label>

                                                                    </div>
                                                                </div>
                                                                <small class="col-sm-8 col-sm-offset-4  m-t-7"><em>During new sales entry you can enter description about each item.</em></small>

                                                            </div>

                                                            <div class="form-group">
                                                                <label class="control-label col-sm-4 ">Enable Discount &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Enabling discount will editable in sales."></i></label>
                                                                <div class="col-sm-8 ">
                                                                    <div class="checkbox checkbox-success">
                                                                        <input id="chkDiscount" type="checkbox" />
                                                                        <label for="chkDiscount">
                                                                        </label>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="control-label col-sm-4 ">Enable Negative Billing</label>
                                                                <div class="col-sm-8">
                                                                    <div class="checkbox checkbox-success">
                                                                        <input id="chkEnableNegativeBilling" type="checkbox" />
                                                                        <label for="chkEnableNegativeBilling">
                                                                        </label>

                                                                    </div>
                                                                </div>
                                                                <small class="col-sm-8 col-sm-offset-4  m-t-7"><em>Enabling this will allow Negative billing without stock checking.</em></small>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="control-label col-sm-4 ">Display Zero quantity items in lookUp &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Show Zero quantited items in lookUp."></i></label>
                                                                <div class="col-sm-8 ">
                                                                    <div class="checkbox checkbox-success">
                                                                        <input id="chkZeroQtyItem" type="checkbox" />
                                                                        <label for="chkZeroQtyItemnt">
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                               <div class="form-group">
                                                                <label class="control-label col-sm-4 ">Default Customer in Sales &nbsp;<i class="md-info-outline" data-toggle="tooltip" data-trigger="hover" title="Select Default Customer"></i></label>
                                                                <div class="col-sm-4 ">
                                                                        <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server"></asp:DropDownList>
                                                                        <label for="Customer">
                                                                        </label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <%-- Price editing settings --%>
                                                    <div class="col-lg-12">
                                                        <h5 class="sett-title ">Allow Price Editing </h5>
                                                        <div class="col-sm-4">
                                                            <div class="form-horizontal">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Purchase request</label>
                                                                    <div class="col-sm-7">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditPReq" type="checkbox" />
                                                                            <label for="chkPriceEditPReq"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Purchase Order &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditPO" type="checkbox" />
                                                                            <label for="chkPriceEditPO"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Purchase Invoice &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditPI" type="checkbox" />
                                                                            <label for="chkPriceEditPI"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Purchase Return</label>
                                                                    <div class="col-sm-7">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditPR" type="checkbox" />
                                                                            <label for="chkPriceEditPR"></label>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="form-horizontal">
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Sales Request &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditSReq" type="checkbox" />
                                                                            <label for="chkZeroQtyItemnt"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Sales Order &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditSO" type="checkbox" />
                                                                            <label for="chkPriceEditSO"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Sales Invoice &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditSE" type="checkbox" />
                                                                            <label for="chkPriceEditSE"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="control-label col-sm-5 ">Sales Return &nbsp;</label>
                                                                    <div class="col-sm-7 ">
                                                                        <div class="checkbox checkbox-success">
                                                                            <input id="chkPriceEditSR" type="checkbox" />
                                                                            <label for="chkPriceEditSR"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12">
                                                        
                                                            <div class="btn-group m-t-20 p-b-15">
                                                                <button type="button" id="btnUpdateOtherSetting" class="btn btn-blue waves-effect waves-light btn-xs"><i class="fa fa-check"></i>&nbsp;<span>Update Price Settings</span></button>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <%-- Invoice Template --%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Terms and condetion Html Reder modal--%>
    <div id="TCModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">
            <!-- Modal content-->
            <div class="modal-content" style="min-height: 445px;">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Type Your Terms & Conditions &nbsp;</h4>
                </div>

                <div style="max-height: 500px" id="summernoteTermsAndCondition">
                </div>
                <button type="button" id="btnsaveTCtoDiv" class="btn btn-default">Save&nbsp; <i class="fa fa-check"></i></button>
                <label id="targetDiv" style="display: none"></label>

            </div>
        </div>
    </div>
    <%--Terms and condetion Html Reder modal ends here--%>

    <script>
        $(document).ready(function () {
            //Nice scroll organisation-wrap
            $(".organisation-wrap").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });

            //Update in Other setting
            $('#btnUpdateOtherSetting').click(function () {
                var data = {};
                if ($('#chkDescription').is(':checked')) {
                    data.EnableDescription = 'True';
                }
                else {
                    data.EnableDescription = 'False';
                }
                if ($('#chkDiscount').is(':checked')) {
                    data.Discount = 'True';
                }
                else {
                    data.Discount = 'False';
                }
                if ($('#chkEnableNegativeBilling').is(':checked')) {
                    data.AllowNegativeBilling = 'True';
                }
                else {
                    data.AllowNegativeBilling = 'False';
                }
                if ($('#chkZeroQtyItem').is(':checked')) {
                    data.DisplayZeroQtyLookup = 'True';
                }
                else {
                    data.DisplayZeroQtyLookup = 'False';
                }
                if ($('#chkPriceEditPReq').is(':checked')) {
                    data.AllowPriceEditingInPurchaseRequest = 'True';
                }
                else {
                    data.AllowPriceEditingInPurchaseRequest = 'False';
                }
                if ($('#chkPriceEditPO').is(':checked')) {
                    data.AllowPriceEditInPurchaseQuote = 'True';
                }
                else {
                    data.AllowPriceEditInPurchaseQuote = 'False';
                }

                if ($('#chkPriceEditPI').is(':checked')) {
                    data.AllowPriceEditInPurchaseEntry = 'True';
                }
                else {
                    data.AllowPriceEditInPurchaseEntry = 'False';
                }

                if ($('#chkPriceEditPR').is(':checked')) {
                    data.AllowPriceEditInPurchaseReturn = 'True';
                }
                else {
                    data.AllowPriceEditInPurchaseReturn = 'False';
                }
                if ($('#chkPriceEditSReq').is(':checked')) {
                    data.AllowPriceEditInSalesRequest = 'True';
                }
                else {
                    data.AllowPriceEditInSalesRequest = 'False';
                }
                if ($('#chkPriceEditSO').is(':checked')) {
                    data.AllowPriceEditInSalesQuote = 'True';
                }
                else {
                    data.AllowPriceEditInSalesQuote = 'False';
                }
                if ($('#chkPriceEditSE').is(':checked')) {
                    data.AllowPriceEditInSalesEntry = 'True';
                }
                else {
                    data.AllowPriceEditInSalesEntry = 'False';
                }
                if ($('#chkPriceEditSR').is(':checked')) {
                    data.AllowPriceEditInSalesReturn = 'True';
                }
                else {
                    data.AllowPriceEditInSalesReturn = 'False';
                }
                data.ModifiedBy = $.cookie('bsl_3');
                data.DefaultCustomer = $('#ddlCustomer').val();
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateOtherSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            });
            //End of update in Others setting

            //******Summer Note initialization for T&C******
            //Summer  note initalization

            //Edit T&C
            $('.btnEditTcPo').click(function () {
                $('#TCModal').modal('show');
                var targetdivId = $(this).parent('h5').siblings('div.editable-div').attr('id');
                //adding code from text area to modal
                $('#summernoteTermsAndCondition').summernote('code', $('#' + targetdivId).html());

                $('#targetDiv').text(targetdivId);//adding target div to hidden feild
                $('#summernoteTermsAndCondition').summernote({
                    placeholder: '',
                    height: 800,
                    airMode: true,
                    popover: {
                        air: [
                          ['color', ['color']],
                          ['font', ['bold', 'underline', 'clear']]
                        ]
                    }
                });
            })

            //Add Terms and condition
            $('#btnsaveTCtoDiv').click(function () {

                var targetDiv = $('#targetDiv').text();
                var html = $('#summernoteTermsAndCondition').summernote('code');
                $('#' + targetDiv).text('');
                $('#' + targetDiv).append(html);
                $('#TCModal').modal('hide');
            })
            //end of Summer  note initalization
            //******Summer Note initialization for T&C ends here******
        
         

            //load terms and conditions
            $('#tandCTab').click(function () {
                LoadTandCSettings();
                $(".terms-conditions").niceScroll({
                    cursorcolor: "#90A4AE",
                    cursorwidth: "8px",
                    horizrailenabled: false
                });
            })

            //load Others settings
            $('#priceEditingTab').click(function () {
                LoadOtherSetting();
                $('#allowPriceEditingSetting').niceScroll({
                    cursorcolor: "#90A4AE",
                    cursorwidth: "8px",
                    horizrailenabled: false
                });
            })

            //click in image template
            $('#vatTemp').click(function () {
                $('.thumbnail').removeClass('selected');
                $(this).addClass('selected');

                $('#VAT').prop('checked', true);
                exitFullscreen();
            });
            $('#gstTemp').click(function () {
                $('.thumbnail').removeClass('selected');
                $(this).addClass('selected');
                $('#GST').prop('checked', true)
                exitFullscreen();
            });
            $('#gstTempTwo').click(function () {
                $('.thumbnail').removeClass('selected');
                $(this).addClass('selected');
                $('#GST2').prop('checked', true)
                exitFullscreen();
            });
            $('#vatTemp2').click(function () {
                $('.thumbnail').removeClass('selected');
                $(this).addClass('selected');
                $('#VAT2').prop('checked', true)
                exitFullscreen();
            });
            $('#vatTemp3').click(function () {
                $('.thumbnail').removeClass('selected');
                $(this).addClass('selected');
                $('#VAT3').prop('checked', true)
                exitFullscreen();
            });
            //end of click in image

            //click for preview
            $('#prevVat').click(function () { launchFullScreen(document.getElementById('vatTemp')); })
            $('#prevVATTwo').click(function () { launchFullScreen(document.getElementById('vatTemp2')); })
            $('#prevVATThree').click(function () { launchFullScreen(document.getElementById('vatTemp3')); })
            $('#prevGst').click(function () { launchFullScreen(document.getElementById('gstTemp')); })
            $('#prevGstTwo').click(function () { launchFullScreen(document.getElementById('gstTempTwo')); })
            $('#prevGstThree').click(function () { launchFullScreen(document.getElementById('gstTempThree')); })
            //end of click of preview

            //Check for query string for load tab
            if ($('#hdSection').val() == 'invoice') {
                $('#tabInvoiceSetting').trigger('click', LoadTemplateSettings());
            }
            else if ($('#hdSection').val() == 'organizationProfile') {
                $('#tabOrgProfile').trigger('click', LoadOrganizationProfile());
            }
            else if ($('#hdSection').val() == 'generalsetting') {
                $('#tabGenSetting').trigger('click', LoadGeneralSettings());
            }
            else if ($('#hdSection').val() == 'finantialsetting') {
                $('#tabFinSetting').trigger('click', LoadFinantialSettings());
            }
            else if ($('#hdSection').val() == 'emailsetting') {
                $('#tabEmailSetting').trigger('click', LoadEmailSettings());
            }

            //Load organization profile in page load if no query string
            LoadOrganizationProfile();
            //Click in the tab
            $('#tabOrgProfile').click(function () { LoadOrganizationProfile(); });
            $('#tabGenSetting').click(function () { LoadGeneralSettings(); });
            $('#tabFinSetting').click(function () { LoadFinantialSettings(); });
            $('#tabEmailSetting').click(function () { LoadEmailSettings(); });
            $('#tabInvoiceSetting').click(function () { LoadTemplateSettings(); });

            //Loading Other Setting
            function LoadOtherSetting() {
                var CompanyId = $.cookie('bsl_1');
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (response) {
                        console.log(response);
                        if (response != null) {

                            $(response).each(function (index) {
                                if (response[index].KeyId == '113') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkDescription').prop('checked', true)
                                    }
                                    else {
                                        $('#chkDescription').prop('checked', false)

                                    }
                                }

                                if (response[index].KeyId == '150') {
                                    
                                    $('#ddlCustomer').select2('val', response[index].KeyValue);
                                }

                                if (response[index].KeyId == '114') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkDiscount').prop('checked', true)
                                    }
                                    else {
                                        $('#chkDiscount').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '132') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkEnableNegativeBilling').prop('checked', true)
                                    }
                                    else {
                                        $('#chkEnableNegativeBilling').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '133') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkZeroQtyItem').prop('checked', true)
                                    }
                                    else {
                                        $('#chkZeroQtyItem').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '135') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditPReq').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditPReq').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '136') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditPO').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditPO').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '137') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditPI').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditPI').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '138') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditPR').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditPR').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '142') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditSReq').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditSReq').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '139') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditSO').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditSO').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '140') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditSE').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditSE').prop('checked', false)

                                    }
                                }
                                if (response[index].KeyId == '141') {
                                    if (response[index].KeyValue == 'True') {
                                        $('#chkPriceEditSR').prop('checked', true)
                                    }
                                    else {
                                        $('#chkPriceEditSR').prop('checked', false)

                                    }
                                }
                            });
                        }

                    },

                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }

            //Loading Organization Profile
            function LoadOrganizationProfile() {
                var CompanyId = $.cookie('bsl_1');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Preferences/Get/?Id=' + CompanyId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    success: function (data) {

                        $('#txtCompanyName').val(data.Name);
                        $('#txtCompanyEmail').val(data.Email);
                        $('#ddlCompanyType').val(data.TypeOfFirm);
                        $('#txtRegId1').val(data.RegId1);
                        $('#txtRegId2').val(data.RegId2);
                        $('#txtRegId3').val(data.RegId3);
                        $('#txtOfficeNo').val(data.OfficeNo);
                        $('#txtPhone1').val(data.MobileNo1);
                        $('#txtphone2').val(data.MobileNo2);
                        $('#txtPincode').val(data.PinCode);
                        $('#txtAddress1').val(data.Address1);
                        $('#txtAddress2').val(data.Address2);
                        $('#ddlCountryList').val(data.CountryId);
                        $('#ddlState').val(data.StateId);
                        $('#txtCity').val(data.City);
                        $("#imgphoto").prop('src', 'data:image/png;base64,' + data.PhotoBase64)
                        $.ajax({
                            url: $('#hdApiUrl').val() + '/api/Preferences/getStates/' + data.CountryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (datas) {
                                $('#ddlCountryList').val();
                                $('#ddlState').children('option').remove();
                                $('#ddlState').append('<option value="0">--select--</option>');
                                $(datas).each(function () {
                                    $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });
                                $('#ddlState').val(data.StateId);

                            }
                            , error: function (xhr) {
                                alert(xhr.responseText);
                                miniLoading('stop');
                            },
                            beforeSend: function () { miniLoading('start'); },
                            complete: function () { miniLoading('stop'); },
                        });
                    },

                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            }
            //Loading states in Change of Country
            $('#ddlCountryList').change(function () {
                var CompanyId = $.cookie('bsl_1');
                var FinancialYear = $.cookie('bsl_4');
                var countryId = $('#ddlCountryList').val();

                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/getStates/' + countryId,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (data) {
                        $('#ddlCountryList').val();
                        $('#ddlState').children('option').remove();
                        $('#ddlState').append('<option value="0">--select--</option>');
                        $(data).each(function () {
                            $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                        });

                    }
                });
            });
            $('#FileUpload').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    // get loaded data and render thumbnail.
                    document.getElementById("imgphoto").src = e.target.result;
                };
                reader.readAsDataURL(this.files[0]);
            });
            //Updating Organization Profile
            $('#btnSaveOrgProfile').off().click(function () {

                var data = {};
                data.ID = $.cookie('bsl_1');
                data.Name = $('#txtCompanyName').val();
                data.Email = $('#txtCompanyEmail').val();
                data.TypeOfFirm = $('#ddlCompanyType').val();
                data.RegId1 = $('#txtRegId1').val();
                data.RegId2 = $('#txtRegId2').val();
                data.RegId3 = $('#txtRegId3').val();
                data.OfficeNo = $('#txtOfficeNo').val();
                data.MobileNo1 = $('#txtPhone1').val();
                data.MobileNo2 = $('#txtphone2').val();
                data.PinCode = $('#txtPincode').val();
                data.Address1 = $('#txtAddress1').val();
                data.Address2 = $('#txtAddress2').val();
                data.CountryId = $('#ddlCountryList').val();
                data.StateId = $('#ddlState').val();
                data.City = $('#txtCity').val();
                data.ModifiedBy = $.cookie('bsl_3');
                data.PhotoBase64 = $("#imgphoto").prop('src').split(',')[1];
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateOrganizationProfile',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        console.log(response);
                        if (response.Success) {
                            successAlert(response.Message);

                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });

            })
            //End of Updating Organization Profile
            //updating General Setting
            $('#btnUpdateGenSetting').off().click(function () {
                var data = {};
                data.DayLightSetting = $('#txtDaylightSaving').val();
                data.ModifiedBy = $.cookie('bsl_3')
                if ($('#chkAutoRoundOff').is(':checked')) {
                    data.AutoRoundOffSetting = 'true';
                }
                else {
                    data.AutoRoundOffSetting = 'false';
                }
                data.CurrencySymbolSetting = $('#txtCurrencySymbol').val();


                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateGeneralSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            })
            //End of General Setting
            //updating  Finantial setting
            $('#btnUpdateFinSetting').off().click(function () {
                var data = {};
                data.FinantialYearSetting = $('#ddlFinantialYear').val();
                data.ExpeneceGroupId = $('#txtExpenceGroupId').val();
                data.ModifiedBy = $.cookie('bsl_3')
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateFinantialSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            })
            //End of Finantial setting

            //updating  Email setting
            $('#btnUpdateEmailSetting').off().click(function () {
                var data = {};
                data.EmailId = $('#txtSettingEmail').val();
                data.EmailPassword = $('#txtEmailPassword').val();
                data.HostSetting = $('#txtHost').val();
                data.PortSetting = $('#txtPort').val();
                data.ModifiedBy = $.cookie('bsl_3')
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateEmailSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            })
            //End of Email setting

            //updating  Template setting
            $('#btnUpdateInvTempSetting').off().click(function () {
                var data = {};
                data.InvoiceTemplateType = $('#txtSettingEmail').val();
                if ($('#GST').is(':checked')) {
                    data.InvoiceTemplateType = 'GST';
                    data.InvoiceTemplateNo = $('#lblgstTempTemplateNumber').text();
                }
                    //gst2 is vat c
                else if ($('#GST2').is(':checked')) {
                    data.InvoiceTemplateType = 'VAT';
                    data.InvoiceTemplateNo = $('#lblgstTempTemplateTwoNumber').text();
                }
                else if ($('#VAT2').is(':checked')) {
                    data.InvoiceTemplateType = 'VAT';
                    data.InvoiceTemplateNo = $('#lblvatTempTemplateTwoNumber').text();
                }
                else if ($('#VAT3').is(':checked')) {
                    data.InvoiceTemplateType = 'VAT';
                    data.InvoiceTemplateNo = $('#lblvatTempTemplateThreeNumber').text();
                }
                else {
                    data.InvoiceTemplateType = 'VAT';
                    data.InvoiceTemplateNo = $('#lblvatTempTemplateNumber').text();

                }

                data.ModifiedBy = $.cookie('bsl_3')
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateInvoiceTemplateSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            })
            //End of Template setting
            //updating  T&C setting
            $('#btnUpdateTermsAndCondetionSetting').off().click(function () {
                var data = {};
                data.PurchaseOrderTC = $('#poDiv').html();
                data.PurchaseInvoiceTC = $('#peDiv').html();
                data.PurchaseReturnTC = $('#prDiv').html();
                data.PurchaseIndentTC = $('#piDiv').html();
                data.GRNTC = $('#grnDiv').html();
                data.TransferOutTC = $('#toDiv').html();
                data.TransferInTC = $('#tiDiv').html();
                data.DamageTc = $('#damDiv').html();
                data.PurchaseRequestTC = $('#prqDiv').html();
                data.SalesOrderTC = $('#SoDiv').html();
                data.SalesInvoiceTC = $('#SeDiv').html();
                data.SalesReturnTC = $('#SrDiv').html();
                data.SalesRequestTC = $('#SrqDiv').html();
                //data.TaxInvoiceTC = $('#TiDiv').html();
                //data.SalesQuotationTC = $('#SalesDiv').html();
                if ($('#chkPoTerms').is(':checked')) {
                    data.PurchaseOrderTCStatus = 1;
                }
                else {
                    data.PurchaseOrderTCStatus = 0;
                }
                if ($('#chkPeTerms').is(':checked')) {
                    data.PurchaseInvoiceTCStatus = 1;
                }
                else {
                    data.PurchaseInvoiceTCStatus = 0;
                }
                if ($('#chkPrTerms').is(':checked')) {
                    data.PurchaseReturnTCStatus = 1;
                }
                else {
                    data.PurchaseReturnTCStatus = 0;
                }
                if ($('#chkPrqTerms').is(':checked')) {
                    data.PurchaseRequestTCStatus = 1;
                }
                else {
                    data.PurchaseRequestTCStatus = 0;
                }
                if ($('#chkSoTerms').is(':checked')) {
                    data.SalesOrderTCStatus = 1;
                }
                else {
                    data.SalesOrderTCStatus = 0;
                }
                if ($('#chkSeTerms').is(':checked')) {
                    data.SalesInvoiceTCStatus = 1;
                }
                else {
                    data.SalesInvoiceTCStatus = 0;
                }
                if ($('#chkSrTerms').is(':checked')) {
                    data.SalesReturnTCStatus = 1;
                }
                else {
                    data.SalesReturnTCStatus = 0;
                }
                if ($('#chktoTerms').is(':checked')) {
                    data.TransferOutTCStatus = 1;
                }
                else {
                    data.TransferOutTCStatus = 0;
                }
                if ($('#chktiTerms').is(':checked')) {
                    data.TransferInTCStatus = 1;
                }
                else {
                    data.TransferInTCStatus = 0;
                }
                if ($('#chkdamTerms').is(':checked')) {
                    data.DamageTCStatus = 1;
                }
                else {
                    data.DamageTCStatus = 0;
                }
                if ($('#chkSrqTerms').is(':checked')) {
                    data.SalesRequestTCStatus = 1;
                }
                else {
                    data.SalesRequestTCStatus = 0;
                }
                if ($('#chkpiTerms').is(':checked')) {
                    data.PurchaseIndentTCStatus = 1;
                }
                else {
                    data.PurchaseIndentTCStatus = 0;
                }
                if ($('#chkgrnTerms').is(':checked')) {
                    data.GRNTCStatus = 1;
                }
                else {
                    data.GRNTCStatus = 0;
                }
                //if ($('#chkTiTerms').is(':checked')) {
                //    data.TaxInvoiceTCStatus = 1;
                //}
                //else {
                //    data.TaxInvoiceTCStatus = 0;
                //}
                //if ($('#chkSalesTerms').is(':checked')) {
                //    data.SalesQuotationTC_Status = 1;
                //}
                //else {
                //    data.SalesQuotationTC_Status = 0;
                //}
                data.ModifiedBy = $.cookie('bsl_3')
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/UpdateTermsAndConditonSetting',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);

                        }

                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            })
            //End of T&C setting

            //Loading T&C Settings
            function LoadTandCSettings() {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',

                    success: function (response) {
                        if (response != null) {
                            $(response).each(function (index) {

                                if (response[index].KeyId == '118') {//Po loading

                                    if (response[index].Status == '1') {
                                        $('#chkPoTerms').prop('checked', true);
                                        $('#poDiv').text('');
                                        $('#poDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkPoTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '144') {//Pi loading

                                    if (response[index].Status == '1') {
                                        $('#chkPiTerms').prop('checked', true);
                                        $('#piDiv').text('');
                                        $('#piDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkPiTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '143') {//grn loading

                                    if (response[index].Status == '1') {
                                        $('#chkgrnTerms').prop('checked', true);
                                        $('#grnDiv').text('');
                                        $('#grnDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkgrnTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '145') {//transferout loading

                                    if (response[index].Status == '1') {
                                        $('#chktoTerms').prop('checked', true);
                                        $('#toDiv').text('');
                                        $('#toDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chktoTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '146') {//transferin loading

                                    if (response[index].Status == '1') {
                                        $('#chktiTerms').prop('checked', true);
                                        $('#tiDiv').text('');
                                        $('#tiDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chktiTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '147') {//transferin loading

                                    if (response[index].Status == '1') {
                                        $('#chkdamTerms').prop('checked', true);
                                        $('#damDiv').text('');
                                        $('#damDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkdamTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '119') {//   Pe loading 
                                    console.log(response[index]);
                                    if (response[index].Status == '1') {
                                        $('#chkPeTerms').prop('checked', true);
                                        $('#peDiv').text('');
                                        $('#peDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkPeTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '120') {//pr loading

                                    if (response[index].Status == '1') {
                                        $('#chkPrTerms').prop('checked', true);
                                        $('#prDiv').text('');
                                        $('#prDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkPrTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '128') {//preq loading

                                    if (response[index].Status == '1') {
                                        $('#chkPrqTerms').prop('checked', true);
                                        $('#prqDiv').text('');
                                        $('#prqDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkPrqTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '115') {//so loading

                                    if (response[index].Status == '1') {
                                        $('#chkSoTerms').prop('checked', true);
                                        $('#SoDiv').text('');
                                        $('#SoDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkSoTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '116') {//se loading

                                    if (response[index].Status == '1') {
                                        $('#chkSeTerms').prop('checked', true);
                                        $('#SeDiv').text('');
                                        $('#SeDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkSeTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '117') {//sr loading

                                    if (response[index].Status == '1') {
                                        $('#chkSrTerms').prop('checked', true);
                                        $('#SrDiv').text('');
                                        $('#SrDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkSrTerms').prop('checked', false);
                                    }

                                }
                                if (response[index].KeyId == '127') {//sr loading

                                    if (response[index].Status == '1') {
                                        $('#chkSrqTerms').prop('checked', true);
                                        $('#SrqDiv').text('');
                                        $('#SrqDiv').append(response[index].KeyValue);
                                    }
                                    else {
                                        $('#chkSrqTerms').prop('checked', false);
                                    }

                                }
                                //if (response[index].KeyId == '149') {//sr loading

                                //    if (response[index].Status == '1') {
                                //        $('#chkSalesTerms').prop('checked', true);
                                //        $('#SalesDiv').text('');
                                //        $('#SalesDiv').append(response[index].KeyValue);
                                //    }
                                //    else {
                                //        $('#chkSalesTerms').prop('checked', false);
                                //    }

                                //}
                                //if (response[index].KeyId == '148') {//sr loading

                                //    if (response[index].Status == '1') {
                                //        $('#chkTiTerms').prop('checked', true);
                                //        $('#TiDiv').text('');
                                //        $('#TiDiv').append(response[index].KeyValue);
                                //    }
                                //    else {
                                //        $('#chkTiTerms').prop('checked', false);
                                //    }

                                //}
                            });

                        }

                    },
                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            };
            //Loading Email Setting
            function LoadEmailSettings() {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',

                    success: function (response) {
                        if (response != null) {
                            $(response).each(function (index) {

                                if (response[index].KeyId == '106') {

                                    $('#txtSettingEmail').val(this.KeyValue);
                                }
                                if (response[index].KeyId == '108') {
                                    $('#txtHost').val(response[index].KeyValue);
                                }
                                if (response[index].KeyId == '109') {
                                    $('#txtPort').val(response[index].KeyValue);
                                }
                            });

                        }



                    },
                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            };
            //Loading Finantial Settings
            function LoadFinantialSettings() {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',

                    success: function (response) {
                        if (response != null) {


                            $(response).each(function (index) {

                                if (response[index].KeyId == '100') {

                                    $('#ddlFinantialYear').val(this.KeyValue);
                                }
                                if (response[index].KeyId == '104') {
                                    $('#txtExpenceGroupId').val(response[index].KeyValue);
                                }

                            });

                        }



                    },
                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            };

            //Loading General Settings
            function LoadGeneralSettings() {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',

                    success: function (response) {
                        if (response != null) {

                            $(response).each(function (index) {

                                if (response[index].KeyId == '101') {

                                    $('#txtDaylightSaving').val(this.KeyValue);
                                }
                                if (response[index].KeyId == '110') {
                                    $('#txtCurrencySymbol').val(response[index].KeyValue);
                                }
                                if (response[index].KeyId == '105') {
                                    if (response[index].KeyValue == 'true') {
                                        $('#chkAutoRoundOff').prop('checked', true);

                                    }
                                    else {
                                        $('#chkAutoRoundOff').prop('checked', false);

                                    }
                                }
                            });

                        }



                    },
                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            };
            //Loading Template Settings
            function LoadTemplateSettings() {
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/GetGeneralSettings',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',

                    success: function (response) {
                        console.log(response);
                        if (response != null) {
                            var hasGst = false;
                            var hasVat = false;
                            $(response).each(function (index) {



                                if (response[index].KeyValue == 'GST') {
                                    hasGst = true;
                                }
                                if (response[index].KeyValue == 'VAT') {
                                    hasVat = true;
                                }

                                if (hasGst) {
                                    if (response[index].KeyValue == 'A') {
                                        $('#GST').prop('checked', true);
                                        $('.thumbnail').removeClass('selected');
                                        $('#gstTemp').addClass('selected');
                                    }

                                }

                                if (hasVat) {
                                    if (response[index].KeyValue == 'A') {
                                        $('#VAT').prop('checked', true);
                                        $('.thumbnail').removeClass('selected');
                                        $('#vatTemp').addClass('selected');
                                    }//gst2 is vat c
                                    if (response[index].KeyValue == 'C') {
                                        $('#GST2').prop('checked', true);
                                        $('.thumbnail').removeClass('selected');
                                        $('#gstTempTwo').addClass('selected');
                                    }
                                    if (response[index].KeyValue == 'D') {
                                        $('#VAT2').prop('checked', true);
                                        $('.thumbnail').removeClass('selected');
                                        $('#vatTemp2').addClass('selected');
                                    }
                                    if (response[index].KeyValue == 'E') {
                                        $('#VAT3').prop('checked', true);
                                        $('.thumbnail').removeClass('selected');
                                        $('#vatTemp3').addClass('selected');
                                    }
                                }




                            });

                        }

                    },
                    error: function (err) {
                        console.log(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            };
            //verify E-mail
            $('#btnverifyEmailSetting').click(function () {
                var data = {};
                data.EmailId = $('#txtSettingEmail').val();
                data.EmailPassword = $('#txtEmailPassword').val();
                data.HostSetting = $('#txtHost').val();
                data.PortSetting = $('#txtPort').val();
                data.ModifiedBy = $.cookie('bsl_3')

                if ($('#chkAutoRoundOff').is(':checked')) {
                    data.AutoRoundOffSetting = 'true';
                }
                else {
                    data.AutoRoundOffSetting = 'false';
                }
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Preferences/TestConnections',
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message);

                        }
                        else {
                            errorAlert(response.Message);

                        }
                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr) }
                });
            });
        });
    </script>
   <%-- Summer Note Linking--%>
<%--    <link href="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet" />
    <script src="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.js"></script>--%>
    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>
    <%--    <link href="../Theme/assets/summernote/summernote.css" rel="stylesheet" />
    <script src="../Theme/assets/summernote/summernote.min.js"></script>--%>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
</asp:Content>
