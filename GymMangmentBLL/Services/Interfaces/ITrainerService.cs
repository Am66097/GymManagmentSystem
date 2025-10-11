using GymMangmentBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel CreatedTrainer);

        TrainerViewModel? GetTrainerDetailsById(int TrainerId);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);

        bool UpdateTrainerDetailsById(int TrainerId, TrainerToUpdateViewModel UpdatedTrainer);

        bool RemoveTrainerDetailsById(int TrainerId);


    }
}
