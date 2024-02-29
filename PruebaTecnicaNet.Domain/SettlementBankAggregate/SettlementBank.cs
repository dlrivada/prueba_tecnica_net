﻿namespace PruebaTecnicaNet.Domain.SettlementBankAggregate;

/// <summary>
/// Settlement bank entity
/// </summary>
public class SettlementBank : Entity, IAggregateRoot, ISettlementBankRepository
{
    public string Bic { get; private set; }
    public string Country { get; private set; }
    public string Name { get; private set; }

    // Additional code

    public SettlementBank(string bic, string country, string name)
    {
        // Do some validations
        if (string.IsNullOrEmpty(bic) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(name))
        {
            throw new SettlementBankException.InvalidSettlementBankException();
        }
        if (!IsBicValid(bic))
        {
            throw new SettlementBankException.InvalidBicException(bic);
        }
        if (!IsCountryValid(country))
        {
            throw new SettlementBankException.InvalidCountryException(country);
        }
        if (!IsNameValid(name))
        {
            throw new SettlementBankException.InvalidNameException(name);
        }
        // If the SettlementBank is not valid, throw an exception
        if (!IsValid())
        {
            throw new SettlementBankException.InvalidSettlementBankException();
        }

        Bic = bic;
        Country = country;
        Name = name;
    }

    // Some example methods to validate the properties of the SettlementBank

    public bool IsBicValid(string bic) => bic.Length == 8;

    public bool IsCountryValid(string country) => country.Length == 2;

    public bool IsNameValid(string name) => name.Length > 0;

    // Now we check only the properties of the SettlementBank but in the future we could add more validations
    public bool IsValid() => IsBicValid(Bic) && IsCountryValid(Country) && IsNameValid(Name);

    // Some example methods to update the properties of the SettlementBank

    public void UpdateBic(string bic)
    {
        if (!IsBicValid(bic))
        {
            throw new SettlementBankException.InvalidBicException(bic);
        }
        Bic = bic;

        if (!IsValid())
        {
            throw new SettlementBankException.InvalidSettlementBankException();
        }

        // Add the domain event
        var settlementBankBicUpdatedEvent = new SettlementBankBicUpdatedEvent(this, bic);
        AddDomainEvent(settlementBankBicUpdatedEvent);
    }

    public void UpdateCountry(string country)
    {
        if (!IsCountryValid(country))
        {
            throw new SettlementBankException.InvalidCountryException(country);
        }
        Country = country;

        if (!IsValid())
        {
            throw new SettlementBankException.InvalidSettlementBankException();
        }

        // Add the domain event
        var settlementBankUpdatedEvent = new SettlementBankCountryUpdatedEvent(this, country);
        AddDomainEvent(settlementBankUpdatedEvent);
    }

    public void UpdateName(string name)
    {
        if (!IsNameValid(name))
        {
            throw new SettlementBankException.InvalidNameException(name);
        }
        Name = name;

        if (!IsValid())
        {
            throw new SettlementBankException.InvalidSettlementBankException();
        }

        // Add the domain event
        var settlementBankUpdatedEvent = new SettlementBankNameUpdatedEvent(this, name);
        AddDomainEvent(settlementBankUpdatedEvent);
    }

}