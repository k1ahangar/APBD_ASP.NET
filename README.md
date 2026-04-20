# APBD_ASP.NET
## How to Run and Test the API

1. Open the project in Visual Studio.

2. Run the application (press **F5** or click the green **Run** button).

3. After the application starts, it will display a local URL in the console, for example:

   ```
   https://localhost:64211
   ```

4. Open your web browser and navigate to the following endpoint:

   ```
   https://localhost:64211/api/rooms
   ```

5. If everything is working correctly, you should see a JSON response containing the list of available rooms.

---

## Additional Testing (Optional)

* You can test other endpoints by changing the URL, for example:

  * `/api/rooms/1` → get a specific room
  * `/api/reservations` → get all reservations

* You may also use tools like Postman to send requests (GET, POST, PUT, DELETE) and test full API functionality.

---

## Notes

* Make sure the application is running before sending requests.
* The port number (e.g., `64211`) may vary depending on your setup.
