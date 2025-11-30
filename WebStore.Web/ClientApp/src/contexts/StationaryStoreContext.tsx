import React, { useState } from "react";
import axios from "axios";
import { IStationaryStore } from "../models/IStationaryStore";

const StationaryStoreContext = React.createContext<any>({});

export const StationaryStoreProvider = (props: { children: React.ReactNode }) => {
    const [state, setState] = useState<{ stores: IStationaryStore[] }>({ stores: [] });

    const getStores = async () => {
        try {
            const response = await axios.get<IStationaryStore[]>("/api/StoreApi");
            setState({ stores: response.data });
        } catch (e) { console.error(e); }
    }
    return (
        <StationaryStoreContext.Provider value={{ state, getStores }}>
            {props.children}
        </StationaryStoreContext.Provider>
    );
}
export default StationaryStoreContext;