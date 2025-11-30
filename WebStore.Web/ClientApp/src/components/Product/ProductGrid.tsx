import React, { useContext, useEffect } from "react";
import { DataGrid, GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { IProduct } from "../../models/IProduct";
import { Link } from "react-router-dom";
import ProductContext from "../../contexts/ProductContext";

export const ProductGrid = () => {
    const columns: GridColDef[] = [
        { field: "id", headerName: "ID", width: 50 },
        { field: "name", headerName: "Name", width: 150 },
        { field: "price", headerName: "Price", width: 100 },
        { field: "description", headerName: "Description", width: 200 },
        {
            field: "edit", headerName: "Edit", sortable: false, width: 100,
            renderCell: (params: GridRenderCellParams) => <Link to={`/product/edit/${params.row.id}`} className="btn btn-primary">Edit</Link>
        },
        {
            field: "delete", headerName: "Delete", sortable: false, width: 100,
            renderCell: (params: GridRenderCellParams) => <Link to={`/product/delete/${params.row.id}`} className="btn btn-danger">Delete</Link>
        }
    ];

    const { getProducts, state }: any = useContext(ProductContext);
    useEffect(() => { getProducts(); }, []);

    return (
        <div>
            <Link style={{ marginBottom: "5px" }} to={"/product/add"} className="btn btn-primary">Add Product</Link>
            <div style={{ height: 400, width: "100%" }}>
                <DataGrid rows={state.products} columns={columns} initialState={{ pagination: { paginationModel: { pageSize: 5, page: 0 } } }} pageSizeOptions={[5, 10]} />
            </div>
        </div>
    )
}