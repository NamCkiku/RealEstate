import { Routes } from '@angular/router';
import { AppModule } from './app.module';

export const appRoutes: Routes = [
    { path: '', redirectTo: 'trang-chu.html', pathMatch: 'full' },
    { path: 'trang-chu.html', loadChildren: './home/home.module#HomeModule' },
    { path: 'tim-kiem-map.html', loadChildren: './map/map.module#MapModule' }
]