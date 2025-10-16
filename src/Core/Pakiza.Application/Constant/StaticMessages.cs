namespace Pakiza.Application.Constant;

public class StaticMessages
{
    //status
    public string Approved = "Approved";
    public string Active = "Active";
    public string Rejected = "Rejected";


    #region SR || MSG :: Not Found
    public string NotFound = "Data not found!";
    #endregion

    #region SR || MSG :: Parameter
    public string InvalidParameterId = "Invalid Id, Provide valid id!";
    public string NotFoundRole = "User does not have any role!";
    #endregion

    #region SR || MSG :: Success
    public string DataSavedSuccessfully = "Data saved successfully!";
    public string DataUpdatedSuccessfully = "Data updated successfully!";
    public string DataDeletedSuccessfully = "Data deleted successfully!";
    public string DataNotFound = "Your requested data can not be found! Please try again.";
    public string DataNotFoundParentMenu = "Your requested menus does not have parent! Please try again.";
    public string DataNotFoundMenu = "Menu not found! Please try again.";
    #endregion

    

    #region SR || MSG :: All Failed
    public string FailedSubmit = "Data Submission Failed!";
    public string FailedUpdate = "Modify request failed!";
    public string FailedDelete = "Delete request failed!";
    public string FailedDeleteForDependency = "Delete failed because the requested record has dependencies.";
    public string FailedRequest = "Request Failed!";
    public string FailedRequestPermission = "Request Failed! Permission request model list is empty.";
    public string AlreadyExists = "Already Exists!";
    public string UserNameAlreadyExists = "User name already taken. Try with a different user name!";
    #endregion

    #region SR || MSG :: List Section
    public string Single = "Single item";
    public string DataList = "Data List";
    #endregion


}
