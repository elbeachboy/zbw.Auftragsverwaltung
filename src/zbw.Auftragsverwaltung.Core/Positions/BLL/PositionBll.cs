﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Core.Positions.Interfaces;
using zbw.Auftragsverwaltung.Core.Positions.Contracts;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Core.Positions.BLL
{
    public class PositionBll : IPositionBll
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionBll(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<PositionDto> Get(Guid id)
        {
            var order = await _positionRepository.GetByIdAsync(id);
            return _mapper.Map<PositionDto>(order);
        }

        public async Task<PaginatedList<PositionDto>> GetList(Expression<Func<Position, bool>> predicate, int size = 10, int page = 1)
        {
            var orders = await _positionRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<PositionDto>>(orders);
        }

        public async Task<PaginatedList<PositionDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var orders = await _positionRepository.GetPagedResponseAsync(page, size);

            return _mapper.Map<PaginatedList<PositionDto>>(orders);
        }

        public async Task<PositionDto> Add(PositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            position = await _positionRepository.AddAsync(position);

            return _mapper.Map<PositionDto>(position);
        }

        public async Task<bool> Delete(PositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            return await _positionRepository.DeleteAsync(position);
        }

        public async Task<PositionDto> Update(PositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            await _positionRepository.UpdateAsync(position);

            return _mapper.Map<PositionDto>(position);
        }
    }
}
