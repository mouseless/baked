import S from"./B-q9G-Wd.js";import w from"./CAYdnnqU.js";import $ from"./D2OKCmka.js";import{i as g,e as _,p as c,f as p,c as s,b as a,P as k,a as d,r as l,F as v,o as r,_ as x}from"./2lwfFFTD.js";import{u as b}from"./B7tuWIi3.js";import{q as m}from"./qd8yMbKY.js";import{a as q}from"./of5sZTY8.js";import"./DNw1WgFb.js";import"./C4aJ5RSL.js";import"./BnNIxdwi.js";const B={key:0},C={class:"full"},F={key:1,class:"content"},N=g({__name:"default",async setup(O){let t,o;const f=b(),i=([t,o]=_(()=>m().where({_path:"/"}).only(["sections"]).findOne()),t=await t,o(),t),n=([t,o]=_(()=>m("/").only(["_path","title","_dir"]).where({_dir:{$eq:""},_path:{$in:i.sections.map(e=>c(e))}}).find()),t=await t,o(),t);for(const e of n)e._path=p(e._path);return q(n,e=>c(p(i.sections[e]))),f.setSections(n),(e,P)=>{const u=S,h=w,y=$;return r(),s(v,null,[a(u),(e._.provides[k]||e.$route).path==="/"?(r(),s("div",B,[d("article",C,[l(e.$slots,"default",{},void 0,!0)])])):(r(),s("div",F,[a(h,{class:"side"}),d("article",null,[l(e.$slots,"default",{},void 0,!0)])])),a(y)],64)}}}),D=x(N,[["__scopeId","data-v-43e4ab6d"]]);export{D as default};
