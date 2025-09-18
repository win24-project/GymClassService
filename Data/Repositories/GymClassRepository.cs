using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class GymClassRepository(DataContext context) : BaseRepository<GymClassEntity>(context), IGymClassRepository { }