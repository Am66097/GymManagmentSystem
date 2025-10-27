using AutoMapper;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymMangmentBLL.Services.Interfaces;
using GymMangmentBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers == null || !Trainers.Any()) return Enumerable.Empty<TrainerViewModel>(); // Return an empty collection if there are no trainers = []
            #region Before Using AutoMapper Pattern
            //return Trainers.Select(t => new TrainerViewModel()
            //{
            //    Id = t.Id,
            //    Name = t.Name,
            //    Phone = t.PhoneNumber,
            //    Email = t.Email,
            //    Specialty = t.Specialties.ToString(),
            //    Address = new Address
            //    {
            //        BuildingNumber = t.Address.BuildingNumber,
            //        Street = t.Address.Street,
            //        City = t.Address.City
            //    }
            //}); 
            #endregion

            return _mapper.Map<IEnumerable<TrainerViewModel>>(Trainers); // Using AutoMapper Pattern


        }

        public bool CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            if (IsEmailExist(CreatedTrainer.Email) || IsPhoneExist(CreatedTrainer.Phone)) return false;


            try
            {
                #region Before Using AutoMapper Pattern
                //var trainer = new Trainer()
                //{
                //    Name = CreatedTrainer.Name,
                //    Email = CreatedTrainer.Email,
                //    PhoneNumber = CreatedTrainer.Phone,
                //    Specialties = CreatedTrainer.Specialty,
                //    DateOfBirth = CreatedTrainer.DateOfBirth
                //};


                //_unitOfWork.GetRepository<Trainer>().Add(trainer);
                //return _unitOfWork.SaveChanges() > 0; 
                #endregion

                #region After Using AutoMapper Pattern
                var trainer = _mapper.Map<Trainer>(CreatedTrainer);
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public TrainerViewModel? GetTrainerDetailsById(int TrainerId)
        {

            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null) return null;
            #region Before Using AutoMapper Pattern
            //var TrainerViewaModel = new TrainerViewModel()
            //{
            //    Id = trainer.Id,
            //    Name = trainer.Name,
            //    Phone = trainer.PhoneNumber,
            //    Email = trainer.Email,
            //    Specialty = trainer.Specialties.ToString()
            //};
            //return TrainerViewaModel; 
            #endregion

            return _mapper.Map<TrainerViewModel>(trainer); // Using AutoMapper Pattern

        }


       public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null) return null;
            #region Before Using AutoMapper Pattern
            //return new TrainerToUpdateViewModel()
            //{
            //    Name = trainer.Name,
            //    Phone = trainer.PhoneNumber,
            //    Email = trainer.Email,
            //    Specialty = trainer.Specialties,
            //    DateOfBirth = trainer.DateOfBirth,
            //    BuildingNumber = trainer.Address.BuildingNumber,
            //    Street = trainer.Address.Street,
            //    City = trainer.Address.City
            //}; 
            #endregion

            return _mapper.Map<TrainerToUpdateViewModel>(trainer);// Using AutoMapper Pattern
        }

        bool ITrainerService.UpdateTrainerDetailsById(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            try
            {
                #region Before Using AutoMapper Pattern
                //if (IsEmailExist(UpdatedTrainer.Email)|| IsPhoneExist(UpdatedTrainer.Phone))return false;
                //var oldMember = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                //if (oldMember is null) return false;
                //oldMember.Name = UpdatedTrainer.Name;
                //oldMember.Email = UpdatedTrainer.Email;
                //oldMember.PhoneNumber = UpdatedTrainer.Phone;
                //oldMember.DateOfBirth = UpdatedTrainer.DateOfBirth;
                //oldMember.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
                //oldMember.Address.Street = UpdatedTrainer.Street;
                //oldMember.Address.City = UpdatedTrainer.City;
                //oldMember.UpdatedAt = DateTime.Now;
                //return _unitOfWork.SaveChanges() > 0; 
                #endregion

                #region After Using AutoMapper Pattern
                if (IsEmailExist(UpdatedTrainer.Email, TrainerId) || IsPhoneExist(UpdatedTrainer.Phone, TrainerId))
                    return false;


                var oldTrainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (oldTrainer == null) return false;

                _mapper.Map(UpdatedTrainer, oldTrainer); // Using Auto Mapper
                oldTrainer.UpdatedAt = DateTime.Now;                      // Using Manual Mapper

                // تحديث العنوان يدويًا
                oldTrainer.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
                oldTrainer.Address.Street = UpdatedTrainer.Street;
                oldTrainer.Address.City = UpdatedTrainer.City;

                return _unitOfWork.SaveChanges() > 0;
                #endregion


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool RemoveTrainerDetailsById(int TrainerId)
        {
            try
            {

                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (trainer is null || HasActiveSessions(TrainerId)) return false;
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }



        #region Helper Methods

        // للتحقق أثناء الإضافة
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(t => t.Email == email);
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(t => t.PhoneNumber == phone);
        }

        // للتحقق أثناء التعديل (استثناء نفس الـ Trainer)
        private bool IsEmailExist(string email, int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(t => t.Email == email && t.Id != trainerId);
        }

        private bool IsPhoneExist(string phone, int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>()
                .GetAll()
                .Any(t => t.PhoneNumber == phone && t.Id != trainerId);
        }




        private bool HasActiveSessions(int trainerId)
        {
            var currentDate = DateTime.Now;
            return _unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == trainerId && s.StartDate > currentDate).Any();
        }




        #endregion
    }
}
