module Prob5.fs

open EulerHelper.Util

let answ = Seq.fold lcm 1L {1L..20L}