using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflexProject.DTO;
using NetflexProject.Models;

namespace NetflexProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NovelsController : ControllerBase
    {
        private readonly NetflexDB _context;

        public NovelsController(NetflexDB context)
        {
            _context = context;
        }

        // GET: api/Novels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovelsAsync()
        {
            List<Novel> nov = _context.Novels.ToList();
            List<Category> cat = _context.Categories.Include(n => n.Novels).ToList();
            List<Subscription> sub = _context.Subscriptions.ToList();
            List<NovelCategorySubscriptionDTO> novDTO = new List<NovelCategorySubscriptionDTO>();
            foreach (var item in nov)
            {
                NovelCategorySubscriptionDTO dto = new NovelCategorySubscriptionDTO()
                {
                    NovelID = item.NovelID,
                    NovelName = item.NovelName,
                    NovelDescription = item.NovelDescription,
                    NovelRate = item.NovelRate,
                    NovelDate = item.NovelDate,
                    NovelImage = item.NovelImage,
                    NovelFile = item.NovelFile
                };
                foreach (var i in sub)
                {
                    if (item.SubscriptionID == i.SubID)
                    {
                        dto.SubscriptionType = i.Type;
                    }
                }
                foreach (var y in cat)
                {
                    foreach (var x in y.Novels)
                    {
                        if (x.NovelID == item.NovelID)
                            dto.CategoryName = y.Type;
                    }
                }
                novDTO.Add(dto);
            }
            return Ok(novDTO);


        }

        // GET: api/Novels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Novel>> GetNovel(int id)
        {
            Novel nov = _context.Novels.FirstOrDefault(n => n.NovelID == id);
            List<Category> cat = _context.Categories.Include(n => n.Novels).ToList();
            List<Subscription> sub = _context.Subscriptions.ToList();
            NovelCategorySubscriptionDTO novDTO = new NovelCategorySubscriptionDTO()
            {
                NovelID = nov.NovelID,
                NovelName = nov.NovelName,
                NovelDescription = nov.NovelDescription,
                NovelRate = nov.NovelRate,
                NovelDate = nov.NovelDate,
                NovelImage = nov.NovelImage,
                NovelFile = nov.NovelFile
            };
            foreach (var i in sub)
            {
                if (nov.SubscriptionID == i.SubID)
                {
                    novDTO.SubscriptionType = i.Type;
                }
            }
            foreach (var y in cat)
            {
                foreach (var x in y.Novels)
                {
                    if (x.NovelID == nov.NovelID)
                        novDTO.CategoryName = y.Type;
                }
            }
            return Ok(novDTO);

        }

        [HttpGet("/api/Novel/{name}")]
        public async Task<ActionResult<Novel>> GetNovelByname(string name)
        {
            Novel nov = _context.Novels.FirstOrDefault(n => n.NovelName == name);
            List<Category> cat = _context.Categories.Include(n => n.Novels).ToList();
            List<Subscription> sub = _context.Subscriptions.ToList();
            NovelCategorySubscriptionDTO novDTO = new NovelCategorySubscriptionDTO()
            {
                NovelID = nov.NovelID,
                NovelName = nov.NovelName,
                NovelDescription = nov.NovelDescription,
                NovelRate = nov.NovelRate,
                NovelDate = nov.NovelDate,
                NovelImage = nov.NovelImage,
                NovelFile = nov.NovelFile
            };
            foreach (var i in sub)
            {
                if (nov.SubscriptionID == i.SubID)
                {
                    novDTO.SubscriptionType = i.Type;
                }
            }
            foreach (var y in cat)
            {
                foreach (var x in y.Novels)
                {
                    if (x.NovelID == nov.NovelID)
                        novDTO.CategoryName = y.Type;
                }
            }
            return Ok(novDTO);

        }

        //[HttpGet("/api/Nuovel/{categoryid}")]
        //public async Task<ActionResult<IEnumerable<Novel>>> GetNovelbycategoryids(int categoryid)
        //{
        //    List<Novel> nov = _context.Novels.ToList();
        //    List<Category> cat = _context.Categories.Include(n => n.Novels).ToList();
        //    List<Subscription> sub = _context.Subscriptions.ToList();
        //    List<NovelCategorySubscriptionDTO> novDTO = new List<NovelCategorySubscriptionDTO>();
        //    foreach (var item in nov)
        //    {
        //        NovelCategorySubscriptionDTO dto = new NovelCategorySubscriptionDTO()
        //        {
        //            NovelID = item.NovelID,
        //            NovelName = item.NovelName,
        //            NovelDescription = item.NovelDescription,
        //            NovelRate = item.NovelRate,
        //            NovelDate = item.NovelDate,
        //            NovelImage = item.NovelImage,
        //            NovelFile = item.NovelFile
        //        };
        //        foreach (var i in sub)
        //        {
        //            if (item.SubscriptionID == i.SubID)
        //            {
        //                dto.SubscriptionType = i.Type;
        //            }
        //        }
        //        foreach (var y in cat)
        //        {
        //            foreach (var x in y.Novels)
        //            {
        //                if (x.NovelID == item.NovelID)
        //                {
        //                    dto.CategoryName = y.Type;
        //                    dto.CategoryId = y.CategoryID;
        //                }

        //            }
        //        }
        //        if (dto.CategoryId == categoryid)
        //        {
        //            novDTO.Add(dto);
        //        }
        //    }
        //    return Ok(novDTO);
        //}

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Novel>>> AddNovels(NovelCategorySubscriptionAddDTO newNovel)
        {

            var Novel = new Novel()
            {
                NovelName = newNovel.NovelName,
                NovelDescription = newNovel.NovelDescription,
                NovelDate = newNovel.NovelDate,
                NovelImage = newNovel.NovelImage,
                NovelRate = newNovel.NovelRate,
                NovelFile = newNovel.NovelFile,
                SubscriptionID = newNovel.SubscriptionId,
                Categories = new List<Category>()
            };
            var categoryIds = newNovel.CategoriesId;
            foreach (var item in categoryIds)
            {
                var category = await _context.Categories.FindAsync(item);
                if (category == null)
                {
                    return BadRequest($"Category with id {item} not found.");
                }
                Novel.Categories.Add(category);
            }
            _context.Novels.Add(Novel);
            _context.SaveChanges();
            return Ok(newNovel);
        }

        [HttpGet("genre")]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovelsByGenre(int genreId)
        {
            var novels = _context.Novels.Where(novel => novel.Categories.Any(category => category.CategoryID == genreId)).Take(15);
            return Ok(novels);
        }

        //Get most rated
        [HttpGet("highRated")]
        public async Task<ActionResult<IEnumerable<Novel>>> GetMostRated()
        {
            List<Novel> novels = _context.Novels.OrderByDescending(e => e.NovelRate).Take(10).ToList();
            return Ok(novels);

        }

        [HttpGet("ModernNovels")]
        public async Task<ActionResult<IEnumerable<Novel>>> GetModernNovels()
        {
            List<Novel> novels = _context.Novels.OrderByDescending(e => e.NovelDate).Take(15).ToList();
            return Ok(novels);

        }



    }
}
