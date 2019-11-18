using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Revature.Address.Lib
{
    public class Address
    {

    private Guid _id { get; set; }
    private string _street { get; set; }
    private string _city { get; set; }
    private string _state { get; set; }
    private string _country { get; set; }
    private string _zipCode { get; set; }

    public Guid Id
    {
      get => _id;
      set
      {
        if (value == null)
          throw new ArgumentException("Id must be set", nameof(value));

        _id = value;
      }
    }
    public string Street
    {
      get
      {

        if (_street == null)
          throw new ArgumentException("Street is not set", nameof(_street));

        return _street;
      }
      set
      {
        if (value == "")
          throw new ArgumentException("Street must not be empty", nameof(value));

        _street = value;
      }
    }
    public string City
    {
      get
      {
        if (_city == null)
          throw new ArgumentException("City is not set", nameof(_city));

        return _city;
      }
      set
      {
        if (value == "")
          throw new ArgumentException("City must not be empty", nameof(value));

        _city = value;
      }
    }
    public string State
    {
      get
      {

        if (_state == null)
          throw new ArgumentException("State is not set", nameof(_city));

        return _state;
      }
      set
      {
        if (value == "")
          throw new ArgumentException("State must not be empty", nameof(value));

        _state = value;
      }
    }

    public string Country {
      get
      {

        if (_country == null)
          throw new ArgumentException("State is not set", nameof(_city));

        return _country;
      }
      set
      {
        if (value == "")
          throw new ArgumentException("State must not be empty", nameof(value));

        _country = value;
      }
    }


    public string ZipCode
    {
      get
      {

        if (_zipCode == null)
          throw new ArgumentException("Zip code is not set", nameof(_zipCode));

        return _zipCode;
      }
      set
      {
        if (value == "")
          throw new ArgumentException("Zip code must not be empty", nameof(value));

        _zipCode = value;
      }
    }

  }
}
