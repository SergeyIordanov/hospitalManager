using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Exceptions;
using HospitalManager.BLL.Interfaces;
using HospitalManager.DAL.Entities;
using HospitalManager.DAL.Interfaces;

namespace HospitalManager.BLL.Services
{
    public class TempService : ITempService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TempService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public TempDto Get(int id)
        {
            var temp = _unitOfWork.Temps.Get(id);
            var tempDto = Mapper.Map<TempDto>(temp);

            return tempDto;
        }

        public IEnumerable<TempDto> GetAll()
        {
            var temps = _unitOfWork.Temps.GetAll().ToList();
            var tempsDto = Mapper.Map<IEnumerable<TempDto>>(temps);

            return tempsDto;
        }

        public void Create(TempDto tempDto)
        {
            var temp = Mapper.Map<Temp>(tempDto);

            _unitOfWork.Temps.Create(temp);
            _unitOfWork.Save();
        }

        public void Edit(TempDto tempDto)
        {
            var updatingTemp = _unitOfWork.Temps.Get(tempDto.Id);

            if (updatingTemp == null)
            {
                throw new EntityNotFoundException($"There is no Temp with id { tempDto.Id } in the database.", "Temp");
            }

            Mapper.Map(tempDto, updatingTemp);

            _unitOfWork.Temps.Update(updatingTemp);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Temps.Delete(id);
            _unitOfWork.Save();
        }
    }
}