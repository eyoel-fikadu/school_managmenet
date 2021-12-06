using SCMS.DataAccess;
using SMS.SERVICE.DTO;

namespace SMS.SERVICE.ServiceLayer.IService.IAdmission
{
    public interface IUserServiceInternal
    {
        User GetUserByUserName(string userName);
        User GetUserById(int id);
        User GetUserByPhoneNumber(string phoneNumber);
        User GetUserByEmail(string email);
        User AddUser(UserModel user, byte[] passwordHash, byte[] passwordSalt);
        User UpdateUser(UserModel user, byte[] passwordHash, byte[] passwordSalt);
        sp_GetActiveEmployess_Result GetActiveEmployeResult(int userId);
        sp_GetActiveStudents_Result GetActiveStudentResult(int userId);
    }
}