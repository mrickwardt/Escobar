import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { FilialRoutingModule } from './filial-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatTabsModule } from '@angular/material/tabs';
import { FilialComponent } from './filial.component';
import { EditComponent } from './edit/edit.component';
import { MatTableModule } from '@angular/material/table';



@NgModule({
  declarations: [CreateComponent, FilialComponent, EditComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatTabsModule,
    MatIconModule,
    MatTableModule,
    FlexLayoutModule,
    FilialRoutingModule
  ]
})
export class FilialModule { }
