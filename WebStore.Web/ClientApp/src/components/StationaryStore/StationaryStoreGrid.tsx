import React, { useContext, useEffect } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import StationaryStoreContext from "../../contexts/StationaryStoreContext";
import { Link } from "react-router-dom";

export const StationaryStoreGrid = () => {
    const columns: GridColDef[] = [
        { field: "id", headerName: "ID", width: 50 },
        { field: "name", headerName: "Name", width: 200 },
        { field: "addressId", headerName: "Address ID", width: 100 },
    ];

    const { getStores, state }: any = useContext(StationaryStoreContext);
    useEffect(() => { getStores(); }, []);

    return (
        <div>
            <h3>Stationary Stores</h3>
            <div style={{ height: 400, width: "100%" }}>
                <DataGrid rows={state.stores} columns={columns} initialState={{ pagination: { paginationModel: { pageSize: 5, page: 0 } } }} pageSizeOptions={[5]} />
            </div>
        </div>
    )
}