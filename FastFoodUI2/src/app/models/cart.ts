export interface CartItem {

    id: number;

    foodItemId: number;

    foodName: string;

    imageUrl: string;

    price: number;

    quantity: number;

    totalPrice: number;

}

export interface CartResponse {

    items: CartItem[];

    grandTotal: number;

}