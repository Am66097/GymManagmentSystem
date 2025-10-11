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

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        IEnumerable<TrainerViewModel> ITrainerService.GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers == null || !Trainers.Any()) return Enumerable.Empty<TrainerViewModel>(); // Return an empty collection if there are no trainers = []
            return Trainers.Select(t => new TrainerViewModel()
            {
                Id = t.Id,
                Name = t.Name,
                Phone = t.PhoneNumber,
                Email = t.Email,
                Specialty = t.Specialties.ToString(),
                Address = new Address
                {
                    BuildingNumber = t.Address.BuildingNumber,
                    Street = t.Address.Street,
                    City = t.Address.City
                }
            });

        }

        bool ITrainerService.CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            if (IsEmailExist(CreatedTrainer.Email) || IsPhoneExist(CreatedTrainer.Phone)) return false;


            try
            {
                var trainer = new Trainer()
                {
                    Name = CreatedTrainer.Name,
                    Email = CreatedTrainer.Email,
                    PhoneNumber = CreatedTrainer.Phone,
                    Specialties = CreatedTrainer.Specialty,
                    DateOfBirth = CreatedTrainer.DateOfBirth
                };


                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        TrainerViewModel? ITrainerService.GetTrainerDetailsById(int TrainerId)
        {

            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null) return null;
            var TrainerViewaModel = new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Phone = trainer.PhoneNumber,
                Email = trainer.Email,
                Specialty = trainer.Specialties.ToString()
            };
            return TrainerViewaModel;

        }


        TrainerToUpdateViewModel? ITrainerService.GetTrainerToUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null) return null;
            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Phone = trainer.PhoneNumber,
                Email = trainer.Email,
                Specialty = trainer.Specialties,
                DateOfBirth = trainer.DateOfBirth,
                BuildingNumber = trainer.Address.BuildingNumber,
                Street = trainer.Address.Street,
                City = trainer.Address.City
            };

        }

        bool ITrainerService.UpdateTrainerDetailsById(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer)
        {
            try
            {
                if (IsEmailExist(UpdatedTrainer.Email)|| IsPhoneExist(UpdatedTrainer.Phone))return false;
                var oldMember = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (oldMember is null) return false;
                oldMember.Name = UpdatedTrainer.Name;
                oldMember.Email = UpdatedTrainer.Email;
                oldMember.PhoneNumber = UpdatedTrainer.Phone;
                oldMember.DateOfBirth = UpdatedTrainer.DateOfBirth;
                oldMember.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
                oldMember.Address.Street = UpdatedTrainer.Street;
                oldMember.Address.City = UpdatedTrainer.City;
                oldMember.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;


            }
            catch (Exception)
            {
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

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(t => t.PhoneNumber == phone).Any();
        }

        private bool HasActiveSessions(int trainerId)
        {
            var currentDate = DateTime.Now;
            return _unitOfWork.GetRepository<Session>().GetAll(s => s.TrainerId == trainerId && s.StartDate > currentDate).Any();
        }


        #endregion
    }
}
