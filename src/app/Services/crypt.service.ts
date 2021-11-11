import { AES, enc } from 'crypto-ts';
import { Injectable } from '@angular/core';

@Injectable()
export class CryptService {

    encode(text: string, key: string): string{
        return AES.encrypt(text, key).toString();
    }

    decode(text: string, key: string): string{
        return AES.decrypt(text, key).toString(enc.Utf8);
    }

    private utf8_encode(text: string): string {
        let str = "";
        for (var i=0; i<text.length; i++) {
            if (text.charCodeAt(i) > 255)
                str += String.fromCharCode(text.charCodeAt(i)-848);
            else str += text.charAt(i);
        }
        return str;
    }

    private utf8_decode(text: string): string {
        let str = "";
        for (var i=0; i<text.length; i++) {
            if (text.charCodeAt(i) > 127)
                str += String.fromCharCode(text.charCodeAt(i)+848);
            else str += text.charAt(i);
        }
        return str;
    }
}
