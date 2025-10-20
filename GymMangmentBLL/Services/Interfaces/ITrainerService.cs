using GymMangmentBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        public IEnumerable<TrainerViewModel> GetAllTrainers();

        public bool CreateTrainer(CreateTrainerViewModel CreatedTrainer);

        public TrainerViewModel? GetTrainerDetailsById(int TrainerId);

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);

        public bool UpdateTrainerDetailsById(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer);

        public bool RemoveTrainerDetailsById(int TrainerId);


    }
}
