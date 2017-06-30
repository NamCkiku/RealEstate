import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapComponent } from './map.component';
import { RouterModule, Routes } from '@angular/router';
export const routes: Routes = [
  { path: '', component: MapComponent },
]
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [MapComponent]
})
export class MapModule { }
