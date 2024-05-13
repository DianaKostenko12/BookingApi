A web API that allows users to register and select apartments for further booking on selected dates.
- User registration. Users can create accounts by providing their personal details and email address.
- View available apartments. Users can view a list of available apartments, including information on the number of rooms, location, amenities and prices.
- Apartment selection and booking. Users can choose the apartments they like and specify the dates for booking.
- Reservation management. Users can view their active and past bookings and cancel bookings if required.
- Authentication and Authorization. Secure API access by authenticating users and authorizing their actions.

There is no method Add in the ApartmentController, and because of this, the apartments are added directly to the database, because there was not enough time to make the roles.
* AuthController
    * [POST] Register : `{domain}/auth/register`
    * [POST] Login: `{domain}/auth/login`
* ReservationController
    * [POST] Book a reservation : `{domain}/reservations`
    * [GET] Get reservations by user : `{domain}/reservations`
    * [DELETE] Unbook reservation : `{domain}/reservations/{reservationId}`
* ApartmentController
    * [GET] Get all apartments : `{domain}/apartments`
