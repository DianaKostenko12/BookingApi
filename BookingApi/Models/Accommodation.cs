﻿namespace BookingApi.Models
{
    public class Accommodation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight {  get; set; }
        public int NumberOfBeds {  get; set; }
        public string Amenities { get; set; }
        public ICollection<AccommodationReservation> AccommodationReservations { get; set; }
    }
}
