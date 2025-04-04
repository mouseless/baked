/*
MIT License

Copyright (c) 2021 Dan Hulton

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

import { isRef, unref } from "vue";

const isObject = val => val !== null && typeof val === "object";
const isArray = Array.isArray;

/**
 * Deeply unref a value, recursing into objects and arrays.
 *
 * @param {Mixed} val - The value to deeply unref.
 *
 * @return {Mixed}
 */
const deepUnref = val => {
  const checkedVal = isRef(val) ? unref(val) : val;

  if(! isObject(checkedVal)) {
    return checkedVal;
  }

  if(isArray(checkedVal)) {
    return unrefArray(checkedVal);
  }

  return unrefObject(checkedVal);
};

/**
 * Unref a value, recursing into it if it's an object.
 *
 * @param {Mixed} val - The value to unref.
 *
 * @return {Mixed}
 */
const smartUnref = val => {
  // Non-ref object?  Go deeper!
  if(val !== null && ! isRef(val) && typeof val === "object") {
    return deepUnref(val);
  }

  return unref(val);
};

/**
 * Unref an array, recursively.
 *
 * @param {Array} arr - The array to unref.
 *
 * @return {Array}
 */
const unrefArray = arr => {
  const unreffed = [];

  arr.forEach(val => {
    unreffed.push(smartUnref(val));
  });

  return unreffed;
};

/**
 * Unref an object, recursively.
 *
 * @param {Object} obj - The object to unref.
 *
 * @return {Object}
 */
const unrefObject = obj => {
  const unreffed = {};

  // Object? un-ref it!
  Object.keys(obj).forEach(key => {
    unreffed[key] = smartUnref(obj[key]);
  });

  return unreffed;
};

export default function() { return { deepUnref }; }