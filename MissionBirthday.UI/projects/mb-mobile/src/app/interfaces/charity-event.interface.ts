export interface CharityEvent {
    id: number;
    organization: string;
    phoneNumber: string;
    email: string;
    url: string;
    location: Address;
    title: string;
    date: string;
    time: string;
    details: string;

    items: string[];
}

export interface Address {
    street1: string;
    street2: string;
    city: string;
    state: string;
    zip: string;
}