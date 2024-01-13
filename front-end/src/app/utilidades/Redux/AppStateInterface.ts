import { areasStatesInterface } from "./Areas/areasStatesInterface";
import { bestStatesInterface } from "./BestSellers/bestStatesInterface";
import { cartStatesInterface } from "./Cart/cartStatesInterface";
import { categoriesStatesInterface } from "./Categories/categoriesStatesInterface";
import { discountsStatesInterface } from "./Discounts/discountsStatesInterface";
import { lowestCostStatesInterface } from "./LowestCost/lowestCostStatesInterface";
import { productStatesInterface, productsFromAreaInterface, productsFromCategoryInterface, productsFromSubcategoryInterface } from "./Products/productStatesInterface";
import { subcategoriesStatesInterface } from "./Subcategories/subcategoriesStatesInterface";

export interface AppStateInterface{
    cart: cartStatesInterface;
    products: productStatesInterface;
    areas: areasStatesInterface;
    bestSellers: bestStatesInterface;
    categories: categoriesStatesInterface;
    subcategories: subcategoriesStatesInterface;
    discounts: discountsStatesInterface;
    lowestCost: lowestCostStatesInterface;
    productsFromArea: productsFromAreaInterface;
    productsFromCategory: productsFromCategoryInterface;
    productsFromSubcategory: productsFromSubcategoryInterface;
}