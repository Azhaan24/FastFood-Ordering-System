export interface CreateOrder {

  deliveryAddress: string;
  paymentMethod: string;
  items: CreateOrderItem[];

}

export interface CreateOrderItem {

  foodId: number;
  quantity: number;

}