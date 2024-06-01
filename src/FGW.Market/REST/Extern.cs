using FGW.Core.Farm.Entities;
using FGW.Core.Farm.Events;
using FGW.Infrastructure;
using FGW.Web.REST.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FGW.Web.REST;

[ApiController]
[Route("/[action]")]
public class Extern(IEventManager eventManager) : ControllerBase
{
    
    [HttpPost]
    public void Donte([FromBody] Donation donation)
    {
        if (donation.Money > 0)
            eventManager.Publish(Farm.GetInstance(), new DonationEvent(donation.Money, donation.Note));
    }
}