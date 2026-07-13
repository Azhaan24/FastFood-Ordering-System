import { Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home';
import { LoginComponent } from './pages/login/login';
import { RegisterComponent } from './pages/register/register';
import { CartComponent } from './pages/cart/cart';
import { CheckoutComponent } from './pages/checkout/checkout';
import { FoodDetailsComponent } from './pages/food-details/food-details';
import { NotFoundComponent } from './pages/not-found/not-found';
import { DashboardComponent } from './pages/admin/dashboard/dashboard';
import { UsersComponent } from './pages/admin/users/users';
import { OrdersComponent } from './pages/admin/orders/orders';
import { CategoriesComponent } from './pages/admin/categories/categories';
import { FoodsComponent } from './pages/admin/foods/foods';
import { AddFoodComponent } from './pages/admin/add-food/add-food';
import { EditFoodComponent } from './pages/admin/edit-food/edit-food';
import { ProfileComponent } from './pages/profile/profile';

import { authGuard } from './guards/auth-guard';
import { adminGuard } from './guards/admin-guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'admin/dashboard', component: DashboardComponent },
  { path: 'admin/users', component: UsersComponent },
  { path: 'admin/orders', component: OrdersComponent },
  { path:'food/:id', component:FoodDetailsComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
  { path: 'cart', component: CartComponent, canActivate: [authGuard] },
  { path: 'checkout', component: CheckoutComponent, canActivate: [authGuard] },
  { path: 'admin/dashboard', component: DashboardComponent, canActivate: [authGuard] },
  { path: 'admin/users', component: UsersComponent, canActivate: [authGuard] },
  { path: 'orders', component: OrdersComponent, canActivate: [authGuard] },
  { path:'admin/categories', component:CategoriesComponent, canActivate:[authGuard] },
  { path:'admin/foods', component:FoodsComponent, canActivate:[authGuard] },
  { path:'admin/foods/add', component:AddFoodComponent, canActivate:[authGuard] },
  { path:'admin/foods/edit/:id', component:EditFoodComponent, canActivate:[authGuard] },
  { path:'admin/dashboard',component:DashboardComponent, canActivate:[adminGuard] },
  { path:'**', component:NotFoundComponent }
];