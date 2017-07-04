import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HeaderComponent } from './header/header.component';
import { SearchComponent } from './search/search.component';
import { RoomComponent } from './room/room.component';
import { PostComponent } from './post/post.component';
import { RouterModule, Routes } from '@angular/router';
export const routes: Routes = [
  {
    path: '', component: HomeComponent,
    children: [
      {
        path: '',
        component: HeaderComponent,
        outlet: 'header'
      },
      {
        path: '',
        component: SearchComponent,
        outlet: 'search'
      },
      {
        path: '',
        component: RoomComponent,
        outlet: 'room'
      },
      {
        path: '',
        component: PostComponent,
        outlet: 'post'
      }
    ]
  },
  { path: '', loadChildren: './header/header.module#HeaderModule' },
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    HomeComponent,
    HeaderComponent,
    SearchComponent,
    RoomComponent,
    PostComponent
  ]
})
export class HomeModule { }
