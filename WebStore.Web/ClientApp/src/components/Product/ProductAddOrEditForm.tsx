import React, { useEffect, useState } from "react";
import { IProduct } from "../../models/IProduct";
import { Box, TextField, Button, Card, CardActions, CardContent, CardHeader } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";

export const ProductAddOrEditForm = (props: { labelName: string }) => {
    const navigate = useNavigate();
    const params = useParams();
    const [state, setState] = useState<IProduct>({ name: "", description: "", price: 0, weight: 0, categoryId: 1, supplierId: 1 });

    useEffect(() => {
        const id = params["id"] ? parseInt(params["id"]!) : undefined;
        if (id) {
            axios.get<IProduct>(`/api/ProductApi/${id}`).then(res => setState(res.data)).catch(e => console.error(e));
        }
    }, [params]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setState(prev => ({ ...prev, [name]: (name === "price" || name === "weight" || name.endsWith("Id")) ? parseFloat(value) : value }));
    }

    const onSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        state.id ? await axios.put("/api/ProductApi", state) : await axios.post("/api/ProductApi", state);
        navigate("/product");
    }

    return (
        <div className="form-container">
            <Box component="form" sx={{ "& .MuiTextField-root": { m: 1, width: "30ch" } }} onSubmit={onSubmit}>
                <Card>
                    <CardHeader title={props.labelName} />
                    <CardContent>
                        <TextField required label="Name" name="name" value={state.name} onChange={handleChange} />
                        <TextField required label="Description" name="description" value={state.description} onChange={handleChange} />
                        <TextField required type="number" label="Price" name="price" value={state.price} onChange={handleChange} />
                        <TextField required type="number" label="Weight" name="weight" value={state.weight} onChange={handleChange} />
                        <TextField required type="number" label="Category ID" name="categoryId" value={state.categoryId} onChange={handleChange} />
                        <TextField required type="number" label="Supplier ID" name="supplierId" value={state.supplierId} onChange={handleChange} />
                    </CardContent>
                    <CardActions>
                        <Button type="submit" variant="contained">Save</Button>
                        <Button onClick={() => navigate("/product")} variant="contained" color="secondary">Cancel</Button>
                    </CardActions>
                </Card>
            </Box>
        </div>
    );
}