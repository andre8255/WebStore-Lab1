export interface IAddress {
    id?: number;
    city: string;
    zipCode: string;
    buildingNumber: number;
    street: string;
    apartmentNumber?: number;
    country: string;
}