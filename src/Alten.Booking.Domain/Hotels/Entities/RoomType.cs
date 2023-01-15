using System.ComponentModel;

namespace Alten.Booking.Domain.Hotels.Entities;

public enum RoomType
{
    [Description("Room with one bed")]
    SingleBed,
    
    [Description("Room with two beds")]
    TwinBeds,
    
    [Description("Room with a bed for two people")]
    DoubleBed,

    [Description("More than one room (e.g. bedroom and living room")]
    Suite
}