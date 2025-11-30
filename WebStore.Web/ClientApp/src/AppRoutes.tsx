import { Home } from "./components/Home";
import { AddressGrid } from './components/Address/AddressGrid';
import { AddressAdd } from "./components/Address/AddressAdd";
import { AddressEdit } from "./components/Address/AddressEdit";
import { AddressDelete } from "./components/Address/AddressDelete";
import { ProductGrid } from "./components/Product/ProductGrid";
import { ProductAdd } from "./components/Product/ProductAdd";
import { ProductEdit } from "./components/Product/ProductEdit";
import { ProductDelete } from "./components/Product/ProductDelete";
import { StationaryStoreGrid } from "./components/StationaryStore/StationaryStoreGrid";

const AppRoutes = [
  { index: true, element: <Home /> },
  
  // Address
  { path: '/address', element: <AddressGrid /> },
  { path: '/address/add', element: <AddressAdd /> },
  { path: '/address/edit/:id', element: <AddressEdit /> },
  { path: '/address/delete/:id', element: <AddressDelete /> },

  // Product
  { path: '/product', element: <ProductGrid /> },
  { path: '/product/add', element: <ProductAdd /> },
  { path: '/product/edit/:id', element: <ProductEdit /> },
  { path: '/product/delete/:id', element: <ProductDelete /> },

  // Store
  { path: '/store', element: <StationaryStoreGrid /> }
];

export default AppRoutes;