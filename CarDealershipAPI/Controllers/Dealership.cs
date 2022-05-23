﻿using GC_CAR_DEALERSHIP.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealershipAPI.Controllers.Models
{
    [ApiController]
    [Route("[controller]")]
    public class Dealership : Controller
    {
     
        GCCarDealershipDBContext context = new GCCarDealershipDBContext();
        List<Car> searchedCars = new List<Car>();

        [HttpGet("AllCars")]
        public List<Car> AllCars()
        {
            return context.Cars.ToList();
        }

        [HttpGet("SearchByID/{id}")]
        public Car SearchId(int id)
        {
            Car searchedCars = context.Cars.Find(id);

            if (searchedCars != null) 
            {
                return searchedCars;
            }
            else 
            {
                Car badId = new Car() { Make = "This is not a valid id." };
                return badId;
            }
        }

        [HttpGet("SearchByMake/{Make}")]
        public List<Car> SearchMake(string Make)
        {
            searchedCars = context.Cars.Where(car => car.Make == Make).ToList();

            if (searchedCars.Count != 0)
            {
                return searchedCars;
            }
            else
            {
                List<Car> badMake = new List<Car>() { new Car() { Make = "We do not have this make." } };
                return badMake;
            }
        }

        [HttpGet("SearchByModel/{Model}")]
        public List<Car> SearchModel(string Model)
        {
            searchedCars = context.Cars.Where(car => car.Model == Model).ToList();

            if (searchedCars.Count != 0)
            {
                return searchedCars;
            }
            else
            {
                List<Car> badModel = new List<Car>() { new Car() { Make = "We do not have this model." } };
                return badModel;
            }
        }

        [HttpGet("SearchByYear/{Year}")]
        public List<Car> SearchYear(int Year)
        {
            searchedCars = context.Cars.Where(car => car.Year == Year).ToList();

            if (searchedCars.Count != 0)
            {
                return searchedCars;
            }
            else
            {
                List<Car> badYear = new List<Car>() { new Car() { Make = "We do not have any cars from that year." } };
                return badYear;
            }
        }

        [HttpGet("SearchByColor/{Color}")]
        public List<Car> SearchColor(string color)
        {
            searchedCars = context.Cars.Where(car => car.Color == color).ToList();

            if (searchedCars.Count != 0)
            {
                return searchedCars;
            }
            else
            {
                List<Car> badColor = new List<Car>() { new Car() { Make = "We do not have any cars with that color." } };
                return badColor;
            }
        }

        [HttpGet("SearchForCar/{Make}&{Model}&{Year}&{Color}")]
        public List<Car> SearchForCars(string Make = "null", string Model = "null", int Year = 0, string Color = "null")
        { 

            if (Make == "null" && Model == "null" && Year == 0 && Color == "null")
            {
                return searchedCars;
            }
            else
            {
                searchedCars = context.Cars.ToList();
            }

            if (Make != "null") 
            {
                searchedCars.RemoveAll(car => car.Make.ToLower() != Make.ToLower());
            }
            if (Model != "null")
            {
                searchedCars.RemoveAll(car => car.Model.ToLower() != Model.ToLower());
            }
            if (Year != 0)
            {
                searchedCars.RemoveAll(car => car.Year != Year);
            }
            if (Color != "null")
            {
                searchedCars.RemoveAll(car => car.Color.ToLower() != Color.ToLower());
            }

            return searchedCars; 
        }

        [HttpPost("AddCar")]
        public void AddCar(Car input)
        {
            context.Cars.Add(input);

            context.SaveChanges();
        }

        [HttpPut("UpdateCar/{id}")]
        public void UpdateCar(int id, Car car)
        {
            Car carToUpdate = SearchId(id);

            carToUpdate.Make = car.Make;
            carToUpdate.Model = car.Model;
            carToUpdate.Year = car.Year;
            carToUpdate.Color = car.Color;

            context.Update(carToUpdate);
            context.SaveChanges();
        }

        [HttpDelete("DeleteCar/{id}")]
        public void DeleteCar(int id)
        {
            context.Cars.Remove(context.Cars.Find(id));

            context.SaveChanges();
        }
    }
}