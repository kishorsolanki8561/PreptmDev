using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public static class MessageStatus
    {
        public static string Success
        {
            get
            {
                return "Data Retrieved Successfully !";
            }
            set { }
        }
        public static string Error
        {

            get
            {
                return "An Error Occurred !";
            }
            set { }
        }
        public static string JsonNotValid
        {

            get
            {
                return "Json Not Valid !";
            }
            set { }
        }

        public static string ModuleKey
        {

            get
            {
                return "Module Key Invalid !";
            }
            set { }
        }
        public static string Create
        {
            get
            {
                return "Record Created Successfully !";
            }
            set { }
        }
        public static string Update
        {
            get
            {
                return "Data Update Successfully !";
            }
            set { }
        }
        public static string Delete
        {
            get
            {
                return "Data Deleted Successfully !";
            }
            set { }
        }
        public static string NoRecord
        {
            get
            {
                return "No Record Found!";
            }
            set { }
        }
        public static string InvalidData
        {
            get
            {
                return "Invalid Data Passed!";
            }
            set { }
        }
        public static string TransferData
        {
            get
            {
                return "Data Transferd Successfully!";
            }
            set { }
        }

        public static string ImportData
        {
            get
            {
                return "Data Import Successfully!";
            }
            set { }
        }

        public static string Save
        {
            get
            {
                return "Record Saved Successfully";
            }
            set { }
        }

        public static string Exist
        {
            get
            {
                return "Data Already Exist !";
            }
            set { }
        }

        public static string UserreadyExist
        {
            get
            {
                return "UserName Already Exist !";
            }
            set { }
        }

        public static string NameExist
        {
            get
            {
                return "Same Name Already Exist !";
            }
            set { }
        }

        public static string DepartmentSectionExist
        {
            get
            {
                return "Department and Section Already Exist !";
            }
            set { }
        }
        public static string DisplayNameExist
        {
            get
            {
                return "Display name is already exist !";
            }
            set { }
        }
        public static string MenuNameExist
        {
            get
            {
                return "Menu name is already exist !";
            }
            set { }
        }
        public static string GSTNumberExist
        {
            get
            {
                return "GST Number is already exist !";
            }
            set { }
        }

        public static string MobileNumberExist
        {
            get
            {
                return "Mobile Number is already exist !";
            }
            set { }
        }
        public static string CaptionNameExist
        {
            get
            {
                return "Caption name is already exist !";
            }
            set { }
        }



        public static string NameWihDepExist
        {
            get
            {
                return "Same Name With Same Department Already Exist !";
            }
            set { }
        }

        public static string DeptWithYearExist
        {
            get
            {
                return "Same Department And Year Already Exist !";
            }
            set { }
        }

        public static string SchemeExist
        {
            get
            {
                return "Scheme Name Already Exist !";
            }
            set { }
        }
        public static string DepartmentExist
        {
            get
            {
                return "Department Already Exist !";
            }
            set { }
        }

        public static string CategoryExist
        {
            get
            {
                return "Category Already Exist !";
            }
            set { }
        }
        public static string SubCategoryExist
        {
            get
            {
                return "Sub Category Already Exist !";
            }
            set { }
        }
        public static string BlockContentExist
        {
            get
            {
                return "Block Content Already Exist In Recruitment!";
            }
            set { }
        }
        public static string BlockTypeExist
        {
            get
            {
                return "BlockType Already Exist !";
            }
            set { }
        }
        public static string LookupExist
        {
            get
            {
                return "Lookup Already Exist !";
            }
            set { }
        }

        public static string LookupTypeExist
        {
            get
            {
                return "Lookup Type Already Exist !";
            }
            set { }
        }
        public static string PageTypeExist
        {
            get
            {
                return "PageType Already Exist !";
            }
            set { }
        }
        public static string UserTypeExist
        {
            get
            {
                return "UserType Already Exist !";
            }
            set { }
        }
        public static string TitleExist
        {
            get
            {
                return "Title Already Exist !";
            }
            set { }
        }
        public static string SlugUrlExist
        {
            get
            {
                return "Slug Url Already Exist !";
            }
            set { }
        }
        public static string QualificationExist
        {
            get
            {
                return "Qualification Already Exist !";
            }
            set { }
        }
        public static string jobDesignationExist
        {
            get
            {
                return "jobDesignation Already Exist !";
            }
            set { }
        }
        public static string PageComponentExist
        {
            get
            {
                return "Component Name Already Exist !";
            }
            set { }
        }
        public static string ModuleExist
        {
            get
            {
                return "Module Name Already Exist !";
            }
            set { }
        }
        public static string RoleNameExist
        {
            get
            {
                return "Role name is already exist !";
            }
            set { }
        }
        public static string ZoneNameExist
        {
            get
            {
                return "Zone name is already exist !";
            }
            set { }
        }

        public static string NotExist
        {
            get
            {
                return "Data Not Exist !";
            }
            set { }
        }

        public static string UserNOTMAP
        {
            get
            {
                return "User Not Map !";
            }
            set { }
        }

        public static string PSWDNOTMATCH
        {
            get
            {
                return "Email Or Password not matched !";
            }
            set { }
        }

        public static string DeletePSWD
        {
            get
            {
                return "Please enter currect paasword ...!";
            }
            set { }
        }

        public static string ErrorValidation
        {
            get
            {
                return "Required fields are null or empty !";
            }
            set { }
        }

        public static string Uploaded
        {
            get
            {
                return "Record Uploaded Successfully";
            }
            set { }
        }

        public static string StatusUpdate
        {
            get
            {
                return "Status Update Successfully";
            }
            set { }
        }

        public static string StatusProgressUpdate
        {
            get
            {
                return "Progress Status Update Successfully";
            }
            set { }
        }

        public static string StatusProgressRequiredUpdate
        {
            get
            {
                return "Progress Status Required";
            }
            set { }
        }

        public static string StatusProgressNotValidCodeUpdate
        {
            get
            {
                return "Progress Status Code Not Valid.";
            }
            set { }
        }

        public static string MarkPresent
        {
            get
            {
                return "This participant marked as present";
            }
            set { }
        }

        public static string MarkAbsent
        {
            get
            {
                return "This participant marked as absent";
            }
            set { }
        }

        public static string Publish
        {
            get
            {
                return "Record Publish Successfully";
            }
            set { }
        }
        public static string FinalAproval
        {
            get
            {
                return "Record Successfully Sent For Final Aproval";
            }
            set { }
        }

        public static string Lock
        {
            get
            {
                return "Data Lock Successfully";
            }
            set { }
        }

        public static string Login
        {
            get
            {
                return "Login Successfully";
            }
            set { }
        }

        public static string AllowToEdit
        {
            get
            {
                return "Record Allow For Edit Successfully";
            }
            set { }
        }

        public static string BlockToEdit
        {
            get
            {
                return "Record Block For Edit Successfully";
            }
            set { }
        }

        public static string ErrorMandatoryField
        {
            get
            {
                return "Mandatory fields are null or empty";
            }
            set { }
        }

        public static string ExistType
        {
            get
            {
                return "Type Already Available";
            }
            set { }
        }


        public static string UnauthorizedUser
        {

            get
            {
                return "An Error Occurred !";
            }
            set { }
        }
        public static string DistrictNotAvailable
        {

            get
            {
                return "District/Office not assigned to this user !";
            }
            set { }
        }
        public static string EmailSendSuccess
        {
            get
            {
                return "Email Send Successfully";
            }
            set { }
        }
        public static string SMSSendSuccess
        {
            get
            {
                return "SMS Send Successfully";
            }
            set { }
        }

        public static string OTPSendSuccess
        {
            get
            {
                return "OTP Send Successfully";
            }
            set { }
        }
        public static string VerifyOTP
        {
            get
            {
                return "Invalid OTP Entered!";
            }
            set { }
        }
        public static string CancelOrder
        {
            get
            {
                return "Order Cancelled Successfully";
            }
            set { }
        }
        public static string OTPTemplateNotAvailable
        {
            get
            {
                return "OTP Template Not Available ! Please make entry for it";
            }
            set { }
        }
        public static string ExcelFormat
        {
            get
            {
                return "This file format is not supported";
            }
            set { }
        }

        public static string ParticipantExistForLocation
        {
            get
            {
                return "Participants exist for this location !";
            }
            set { }
        }
        public static string ParticipantExistForVC
        {
            get
            {
                return "Participants exist for this VC !";
            }
            set { }
        }
        public static string ExportSuccess
        {
            get
            {
                return "Data exported successfully";
            }
            set { }
        }
        public static string UnthothorizedForActivity
        {
            get
            {
                return "You are not authorized for this activity";
            }
            set { }
        }
        public static string YearMonthExist
        {
            get
            {
                return "Record already exist with same year and month !";
            }
            set { }
        }
        public static string ResetUserSpecificPermission
        {
            get
            {
                return "Specific permission for this user removed successfully !";
            }
            set { }
        }
        public static string NotActiveUser
        {
            get
            {
                return "You have been deactivated by Admin.";
            }
            set { }
        }
        public static string NotVerifiedUser
        {
            get
            {
                return "You have not verified your mobile number.";
            }
            set { }
        }
        public static string MobileNumberExits
        {
            get
            {
                return "Mobile number is already exist !.";
            }
            set { }
        }
        public static string UserNameisExits
        {
            get
            {
                return "User name is already exist !.";
            }
            set { }
        }
        public static string EmailisExits
        {
            get
            {
                return "Email already exist !.";
            }
            set { }
        }
        public static string UserEventExist
        {
            get
            {
                return "You have already applied for this event.";
            }
            set { }
        }
        public static string onlyPDFValid
        {
            get
            {
                return "only upload pdf file";
            }
            set { }
        }

        public static string AddBookmark
        {
            get
            {
                return "Bookmarked Successfully !";
            }
            set { }
        }
        public static string RemoveBookmark
        {
            get
            {
                return "Bookmark Removed Successfully !";
            }
            set { }
        }

        public static string FCMToken
        {
            get
            {
                return "FCM token req !";
            }
            set { }
        }
        public static string DataBaseError
        {
            get
            {
                return "Invalid Data Passed!";
            }
            set { }
        }
        public static string FileDeleted
        {
            get
            {
                return "Document Successfully Deleted!";
            }
            set { }
        }
        public static string FileUpload
        {
            get
            {
                return "File uploaded successfully.";
            }
            set { }
        }
        public static string FileExists
        {
            get
            {
                return "File is not exists";
            }
            set { }
        }
    }
}
