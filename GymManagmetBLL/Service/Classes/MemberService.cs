using GymManagmetBLL.Service.Interfasces;
using GymManagmetBLL.Service.ViewModels.MemberViewModel;
using GymManagmetDAL.Entities;
using GymManagmetDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmetBLL.Service.Classes
{
    internal class MemberService : IMemberService 
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                // check if email or phone already exists
                var existingMembers = IsEmailOrPhoneExists(createMember.Email, createMember.Phone);

                // if one of them exists, return false
                if (existingMembers)
                    return false;

                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createMember.BuildingNumber,
                        Street = createMember.Street,
                        City = createMember.City
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecord.Height,
                        Weight = createMember.HealthRecord.Weight,
                        BloodType = createMember.HealthRecord.BloodType,
                        Note = createMember.HealthRecord.Note
                    }
                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() >0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModels> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any()) return [];

            #region Way-1: Using LINQ foreach
            //var memberViewModels = new List<MemberViewModel>();

            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Photo = member.Photo,
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Email = member.Email,
            //        Gender = member.Gender.ToString()
            //    };
            //    memberViewModels.Add(memberViewModel);
            //    return memberViewModels;
            //}
            #endregion

            #region Way-2: Using LINQ Select

            var memberViewModels = members.Select(member => new MemberViewModels()
            {
                Id = member.Id,
                Photo = member.Photo,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Gender = member.Gender.ToString()
            });
            return memberViewModels;
            #endregion
        }

        public MemberViewModels? GetMemberDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(id);
            if (member is null) return null;

            var memberDetails = new MemberViewModels()
            {
                Name = member.Name,
                Email = member.Email,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("dd/MM/yyyy"),
                Address = $"{member.Address.BuildingNumber}, {member.Address.Street}, {member.Address.City}",
                Phone = member.Phone,
            };
            // get member's active membership
            var ActiveMembership = _unitOfWork.GetRepository<Membership>()
                .GetAll(m => m.MemberId == id && m.Statues == "Active")
                .FirstOrDefault();

            if (ActiveMembership is not null)
            {
                memberDetails.MembershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                memberDetails.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
                var plane = _unitOfWork.GetRepository<Plane>().GetById(ActiveMembership.PlaneId);
                memberDetails.PlaneName = plane?.Name;
            }
            return memberDetails;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int id)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(id);
            if (MemberHealthRecord is null) return null;

            return new HealthRecordViewModel()
            {
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                BloodType = MemberHealthRecord.BloodType,
                Note = MemberHealthRecord.Note
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdateDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(id);
            if (member is null) return null;
            
            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City
            };
        }
       
        public bool UpdateMember(int id, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                // check if email or phone already exists
                var existingMembers = IsEmailOrPhoneExists(memberToUpdate.Email, memberToUpdate.Phone);

                // if one of them exists, return false
                if (existingMembers)
                    return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(id);
                if (member is null) return false;

                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Address.Street = memberToUpdate.Street;
                member.Address.City = memberToUpdate.City;
                member.UpdatedAt = DateTime.Now;

                _unitOfWork.GetRepository<Member>().Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveMember(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(id);

            if (member is null) return false;
            var hasActiveMembership = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(m => m.MemberId == id && m.Session.StartDate > DateTime.Now).Any();

            if (hasActiveMembership) return false; // cannot delete member with active membership

            var MemberShip = _unitOfWork.GetRepository<Membership>()
                .GetAll(m => m.MemberId == id);
            try
            {
                if (MemberShip.Any())
                {
                    foreach (var membership in MemberShip)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(membership);
                    }
                }
                _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Methods
        private bool IsEmailOrPhoneExists(string email, string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == email || m.Phone == phone).Any();
        }
        #endregion
    }
}
