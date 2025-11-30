import React, { useState } from "react";
import axios from "axios";
import { IAddress } from "../models/IAddress";

const AddressContext = React.createContext<any>({});

type IProps = {
    children: React.ReactNode
};

type IState = {
    addresses: IAddress[]
};

export const AddressProvider = (props: IProps) => {
    const [state, setState] = useState<IState>({ addresses: [] });

    const getAddresses = async () => {
        try {
            const response = await axios.get<IAddress[]>("/api/Address");
            setState({ addresses: response.data });
        } catch (e) { console.error("Błąd pobierania adresów", e); }
    }

    const getAddress = async (id: number) => {
        try {
            const response = await axios.get<IAddress>(`/api/Address/${id}`);
            return response.data;
        } catch (e) { console.error("Błąd pobierania adresu", e); return null; }
    }

    const addOrUpdateAddress = async (address: IAddress) => {
        try {
            if (address.id) {
                await axios.put("/api/Address", address);
            } else {
                await axios.post("/api/Address", address);
            }
        } catch (e) { console.error("Błąd zapisu", e); }
    }

    const deleteAddress = async (id: number) => {
        try {
            await axios.delete(`/api/Address/${id}`);
        } catch (e) { console.error("Błąd usuwania", e); }
    }

    return (
        <AddressContext.Provider value={{
            state,
            getAddresses,
            getAddress,
            addOrUpdateAddress,
            deleteAddress
        }}>
            {props.children}
        </AddressContext.Provider>
    );
}

export default AddressContext;