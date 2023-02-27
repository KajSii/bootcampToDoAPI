import { Time } from "@angular/common";

export interface Item {
    publicId?: string;
    name: string;
    timeOfCreation: Date;
    // description: string;
    status?: boolean;
    endTime?: Date;
}