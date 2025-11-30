import React, { useState } from "react";
import axios from "axios";
import { IProduct } from "../models/IProduct";

const ProductContext = React.createContext<any>({});

export const ProductProvider = (props: { children: React.ReactNode }) => {
    const [state, setState] = useState<{ products: IProduct[] }>({ products: [] });

    const getProducts = async () => {
        try {
            const response = await axios.get<IProduct[]>("/api/ProductApi");
            setState({ products: response.data });
        } catch (e) { console.error(e); }
    }

    const getProduct = async (id: number) => {
        try {
            const response = await axios.get<IProduct>(`/api/ProductApi/${id}`);
            return response.data;
        } catch (e) { console.error(e); return null; }
    }

    const addOrUpdateProduct = async (product: IProduct) => {
        try {
            if (product.id) await axios.put("/api/ProductApi", product);
            else await axios.post("/api/ProductApi", product);
        } catch (e) { console.error(e); }
    }

    const deleteProduct = async (id: number) => {
        try { await axios.delete(`/api/ProductApi/${id}`); } 
        catch (e) { console.error(e); }
    }

    return (
        <ProductContext.Provider value={{ state, getProducts, getProduct, addOrUpdateProduct, deleteProduct }}>
            {props.children}
        </ProductContext.Provider>
    );
}
export default ProductContext;