export interface OrderItem {

  foodItemId: number;

  foodName: string;

  quantity: number;

  unitPrice: number;

  totalPrice: number;

}

export interface Order {

  id: number;

  userId: string;

  orderDate: string;

  totalAmount: number;

  status: string;

  deliveryAddress: string;

  items: OrderItem[];

}

export interface CreateOrder {

  deliveryAddress: string;

}