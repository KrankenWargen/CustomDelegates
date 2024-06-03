using FGW.Farm;
using FGW.Farm.Farm.Events;
using FGW.Web.REST.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FGW.Web.REST;

using Farm = Farm.Entities.Farm;

[ApiController]
[Route("/[action]")]
public class Extern() : ControllerBase
{
    [HttpPost]
    public void Donte([FromBody] Donation donation)
    {
        if (donation.Money > 0)
            FarmManager.GetInstance().Publish(Farm.GetInstance(),
                new DonationEvent(donation.Money, donation.Note));
    }
}