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
        private readonly IGenericRepository<Member>       _memberRepository;
        private readonly IGenericRepository<Membership>   _membershipRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;
        private readonly IGenericRepository<MemberSession> _memberSessionRepository;
        private readonly IPlaneRepository                 _planeRepository;

        public MemberService(IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> membershipRepository,
            IGenericRepository<HealthRecord> healthRecordRepository,
            IGenericRepository<MemberSession> memberSessionRepository,
            IPlaneRepository planeRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _healthRecordRepository = healthRecordRepository;
            _memberSessionRepository = memberSessionRepository;
            _planeRepository = planeRepository;
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
                _memberRepository.Add(member);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModels> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
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
            var member = _memberRepository.GetById(id);
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
            var ActiveMembership = _membershipRepository
                .GetAll(m => m.MemberId == id && m.Statues == "Active")
                .FirstOrDefault();

            if (ActiveMembership is not null)
            {
                memberDetails.MembershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                memberDetails.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
                var plane = _planeRepository.GetById(ActiveMembership.PlaneId);
                memberDetails.PlaneName = plane?.Name;
            }
            return memberDetails;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int id)
        {
            var MemberHealthRecord = _healthRecordRepository.GetById(id);
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
            var member = _memberRepository.GetById(id);
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

                var member = _memberRepository.GetById(id);
                if (member is null) return false;
                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Address.Street = memberToUpdate.Street;
                member.Address.City = memberToUpdate.City;
                member.UpdatedAt = DateTime.Now;

                return _memberRepository.Update(member) > 0; // return true if update is successful
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveMember(int id)
        {
            var member = _memberRepository.GetById(id);

            if (member is null) return false;
            var hasActiveMembership = _memberSessionRepository
                .GetAll(m => m.MemberId == id && m.Session.StartDate > DateTime.Now).Any();

            if (hasActiveMembership) return false; // cannot delete member with active membership

            var MemberShip = _membershipRepository
                .GetAll(m => m.MemberId == id);
            try
            {
                if (MemberShip.Any())
                {
                    foreach (var membership in MemberShip)
                    {
                        _membershipRepository.Delete(membership);
                    }
                }
                return _memberRepository.Delete(member) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Methods
        private bool IsEmailOrPhoneExists(string email, string phone)
        {
            return _memberRepository.GetAll(m => m.Email == email || m.Phone == phone).Any();
        }
        #endregion
    }
}
