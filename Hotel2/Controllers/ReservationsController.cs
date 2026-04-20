using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var res = DataStore.Reservations.AsQueryable();

        if (date.HasValue)
            res = res.Where(r => r.Date == date);

        if (!string.IsNullOrEmpty(status))
            res = res.Where(r => r.Status == status);

        if (roomId.HasValue)
            res = res.Where(r => r.RoomId == roomId);

        return Ok(res.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();
        return Ok(res);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
            return NotFound("Room not found");

        if (!room.IsActive)
            return BadRequest("Room is inactive");

        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest("EndTime must be later than StartTime");

        bool overlap = DataStore.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime
        );

        if (overlap)
            return Conflict("Time overlap");

        reservation.Id = DataStore.NextReservationId;
        DataStore.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Reservation updated)
    {
        var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();

        res.RoomId = updated.RoomId;
        res.OrganizerName = updated.OrganizerName;
        res.Topic = updated.Topic;
        res.Date = updated.Date;
        res.StartTime = updated.StartTime;
        res.EndTime = updated.EndTime;
        res.Status = updated.Status;

        return Ok(res);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();

        DataStore.Reservations.Remove(res);
        return NoContent();
    }
}