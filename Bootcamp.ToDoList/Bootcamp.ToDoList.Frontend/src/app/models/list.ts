import { Item } from "./item";

export interface List {
    publicId: string;
    name: string;
    items: Item[];
}