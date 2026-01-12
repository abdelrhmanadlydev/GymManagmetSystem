using GymManagmetBLL.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Interfasces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModels> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModels? GetMemberDetails(int id);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int id);
        MemberToUpdateViewModel? GetMemberToUpdateDetails(int id);
        bool UpdateMember(int id, MemberToUpdateViewModel memberToUpdate);
        bool RemoveMember(int id);
    }
}
